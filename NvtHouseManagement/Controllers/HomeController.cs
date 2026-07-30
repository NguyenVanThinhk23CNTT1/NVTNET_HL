using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NvtHouseManagement.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NvtHouseManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly NvtDbContext _context;

        public HomeController(NvtDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // 1. Thống kê số lượng Phòng
            ViewBag.TongSoPhong = await _context.NvtPhongs.CountAsync();
            ViewBag.SoPhongTrong = await _context.NvtPhongs.CountAsync(p => p.TrangThai == "Trống" || p.TrangThai == null);
            ViewBag.SoPhongDaThue = await _context.NvtPhongs.CountAsync(p => p.TrangThai == "Đã thuê");

            // 2. Thống kê Khách thuê & Hợp đồng
            ViewBag.TongKhachThue = await _context.NvtKhachThues.CountAsync();
            ViewBag.TongHopDongActive = await _context.NvtHopDongs.CountAsync(h => h.TrangThai == "Hiệu lực" || h.TrangThai == null);

            // 3. Thống kê Doanh thu tháng này (Các hóa đơn Đã thanh toán)
            var thangHienTai = DateTime.Now.Month;
            var namHienTai = DateTime.Now.Year;

            var tongDoanhThu = await _context.NvtHoaDons
                .Where(h => h.Thang == thangHienTai && h.Nam == namHienTai && h.TrangThai == "Đã thanh toán")
                .SumAsync(h => (decimal?)h.TongTien) ?? 0m;

            // 4. Hóa đơn gần đây
            var recentHoaDons = await _context.NvtHoaDons
                .Include(h => h.HopDong)
                .ThenInclude(h => h.Phong)
                .OrderByDescending(h => h.Id)
                .Take(4)
                .ToListAsync();
            ViewBag.RecentHoaDons = recentHoaDons;

            // 5. Thống kê lấp đầy theo tầng
            var phongs = await _context.NvtPhongs.ToListAsync();
            var floorStats = phongs.GroupBy(p => p.Tang)
                .Select(g => new FloorStatViewModel
                {
                    Tang = g.Key,
                    TotalRooms = g.Count(),
                    OccupiedRooms = g.Count(p => p.TrangThai == "Đã thuê"),
                    OccupancyRate = g.Count() > 0 ? (g.Count(p => p.TrangThai == "Đã thuê") * 100 / g.Count()) : 0
                })
                .OrderBy(f => f.Tang)
                .ToList();
            
            ViewBag.FloorStats = floorStats;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}