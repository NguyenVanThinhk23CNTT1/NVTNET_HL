using System;
using System.Collections.Generic;

namespace NvtHouseManagement.Models;

public partial class NvtHoaDon
{
    public int Id { get; set; }

    public int HopDongId { get; set; }

    public int Thang { get; set; }

    public int Nam { get; set; }

    public decimal TienPhong { get; set; }

    public decimal TienDien { get; set; }

    public decimal TienNuoc { get; set; }

    public decimal? TienDichVuKhac { get; set; }

    public decimal TongTien { get; set; }

    public string TrangThai { get; set; } = null!;

    public DateTime? NgayTao { get; set; }

    public DateTime? NgayThanhToan { get; set; }

    public virtual NvtHopDong HopDong { get; set; } = null!;
}
