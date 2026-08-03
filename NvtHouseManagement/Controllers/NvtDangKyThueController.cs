using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NvtHouseManagement.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NvtHouseManagement.Controllers
{
    public class NvtDangKyThueController : Controller
    {
        private readonly NvtDbContext _context;

        public NvtDangKyThueController(NvtDbContext context)
        {
            _context = context;
        }

        // GET: Hiển thị form đăng ký
        public async Task<IActionResult> Index(int? selectedPhongId = null)
        {
            // Lấy danh sách phòng trống
            var phongTrongs = await _context.NvtPhongs
                .Include(p => p.LoaiPhong)
                .Where(p => p.TrangThai == "Trống" || p.TrangThai == "Empty")
                .Select(p => new {
                    Id = p.Id,
                    TenPhieu = p.SoPhong + " - Giá: " + string.Format("{0:#,##0}", p.LoaiPhong != null ? p.LoaiPhong.GiaCoBan : 0) + " VNĐ"
                }).ToListAsync();

            ViewBag.PhongId = new SelectList(phongTrongs, "Id", "TenPhieu", selectedPhongId);

            var model = new DangKyThueViewModel
            {
                PhongId = selectedPhongId ?? 0,
                NgayBatDau = DateOnly.FromDateTime(DateTime.Now),
                NgayKetThuc = DateOnly.FromDateTime(DateTime.Now.AddMonths(6))
            };

            return View(model);
        }

        // POST: Xử lý submit form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(DangKyThueViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                ModelState.AddModelError("", "Dữ liệu nhập chưa hợp lệ: " + string.Join("; ", errors));
            }

            if (ModelState.IsValid)
            {
                // Bắt đầu Transaction
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // 1. Tạo Khách Thuê mới
                    var khachThue = new NvtKhachThue
                    {
                        HoTen = model.HoTen,
                        SoDienThoai = model.SoDienThoai,
                        Cccd = model.Cccd,
                        Email = model.Email,
                        QueQuan = model.QueQuan
                    };
                    _context.NvtKhachThues.Add(khachThue);
                    await _context.SaveChangesAsync(); // Cấp ID mới cho Khách

                    // 2. Tạo Hợp Đồng mới
                    var hopDong = new NvtHopDong
                    {
                        KhachThueId = khachThue.Id,
                        PhongId = model.PhongId,
                        NgayBatDau = model.NgayBatDau,
                        NgayKetThuc = model.NgayKetThuc,
                        TienCoc = model.TienCoc,
                        GiaThueThucTe = model.GiaThueThucTe,
                        TrangThai = "Hiệu lực"
                    };
                    _context.NvtHopDongs.Add(hopDong);
                    await _context.SaveChangesAsync(); // Cấp ID mới cho Hợp đồng

                    // 3. Tạo Hóa Đơn tháng đầu tiên
                    var hoaDon = new NvtHoaDon
                    {
                        HopDongId = hopDong.Id,
                        Thang = DateTime.Now.Month,
                        Nam = DateTime.Now.Year,
                        TienPhong = model.GiaThueThucTe, // Thu luôn tiền phòng tháng đầu
                        TienDien = 0,
                        TienNuoc = 0,
                        TienDichVuKhac = model.TienDichVuKhac,
                        TongTien = model.TienCoc + model.GiaThueThucTe + model.TienDichVuKhac, // Cọc + Phòng tháng đầu
                        TrangThai = "Chưa thanh toán",
                        NgayTao = DateTime.Now
                    };
                    _context.NvtHoaDons.Add(hoaDon);

                    // 4. Cập nhật trạng thái Phòng
                    var phong = await _context.NvtPhongs.FindAsync(model.PhongId);
                    if (phong != null)
                    {
                        phong.TrangThai = "Đang thuê";
                        _context.NvtPhongs.Update(phong);
                    }

                    // Lưu Hóa đơn và Cập nhật phòng
                    await _context.SaveChangesAsync();

                    // Chốt giao dịch
                    await transaction.CommitAsync();

                    // Thành công: Trở về trang thông báo hoặc redirect
                    TempData["SuccessMessage"] = $"Đăng ký thành công! Hệ thống đã tạo Hợp đồng #{hopDong.Id} và Hóa đơn #{hoaDon.Id} cho khách hàng {khachThue.HoTen}.";
                    return RedirectToAction("Index", "NvtHopDong"); // Chuyển sang trang ds hợp đồng
                }
                catch (Exception ex)
                {
                    // Lỗi: Rollback toàn bộ
                    await transaction.RollbackAsync();
                    string innerErr = ex.InnerException != null ? ex.InnerException.Message : "";
                    ModelState.AddModelError("", $"Lỗi Database: {ex.Message}. Chi tiết: {innerErr}");
                }
            }

            // Nếu lỗi, load lại danh sách phòng
            var phongTrongs = await _context.NvtPhongs
                .Include(p => p.LoaiPhong)
                .Where(p => p.TrangThai == "Trống" || p.TrangThai == "Empty")
                .Select(p => new {
                    Id = p.Id,
                    TenPhieu = p.SoPhong + " - Giá: " + string.Format("{0:#,##0}", p.LoaiPhong != null ? p.LoaiPhong.GiaCoBan : 0) + " VNĐ"
                }).ToListAsync();
            ViewBag.PhongId = new SelectList(phongTrongs, "Id", "TenPhieu", model.PhongId);

            return View(model);
        }
    }
}
