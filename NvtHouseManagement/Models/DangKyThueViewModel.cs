using System;
using System.ComponentModel.DataAnnotations;

namespace NvtHouseManagement.Models
{
    public class DangKyThueViewModel
    {
        // --- 1. Thông tin Khách Thuê ---
        [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
        public string HoTen { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [RegularExpression(@"^(0[3|5|7|8|9])+([0-9]{8})$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string SoDienThoai { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập số CCCD.")]
        [StringLength(12, MinimumLength = 9, ErrorMessage = "CCCD phải từ 9-12 số.")]
        public string Cccd { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string? Email { get; set; }

        public string? QueQuan { get; set; }

        // --- 2. Thông tin Hợp Đồng ---
        [Required(ErrorMessage = "Vui lòng chọn phòng.")]
        public int PhongId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày bắt đầu.")]
        public DateOnly NgayBatDau { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [Required(ErrorMessage = "Vui lòng chọn ngày kết thúc.")]
        public DateOnly NgayKetThuc { get; set; } = DateOnly.FromDateTime(DateTime.Now.AddMonths(6));

        [Required(ErrorMessage = "Vui lòng nhập tiền cọc.")]
        public decimal TienCoc { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá thuê thực tế.")]
        public decimal GiaThueThucTe { get; set; }
        
        // Tiền dịch vụ phát sinh thêm ngay tháng đầu tiên (nếu có)
        public decimal TienDichVuKhac { get; set; } = 0;
    }
}
