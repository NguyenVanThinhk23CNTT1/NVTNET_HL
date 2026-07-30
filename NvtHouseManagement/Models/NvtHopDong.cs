using System;
using System.Collections.Generic;

namespace NvtHouseManagement.Models;

public partial class NvtHopDong
{
    public int Id { get; set; }

    public int PhongId { get; set; }

    public int KhachThueId { get; set; }

    public DateOnly NgayBatDau { get; set; }

    public DateOnly NgayKetThuc { get; set; }

    public decimal TienCoc { get; set; }

    public decimal GiaThueThucTe { get; set; }

    public string TrangThai { get; set; } = null!;

    public virtual NvtKhachThue KhachThue { get; set; } = null!;

    public virtual ICollection<NvtChiTietDichVu> NvtChiTietDichVus { get; set; } = new List<NvtChiTietDichVu>();

    public virtual ICollection<NvtHoaDon> NvtHoaDons { get; set; } = new List<NvtHoaDon>();

    public virtual ICollection<NvtThanhVienPhong> NvtThanhVienPhongs { get; set; } = new List<NvtThanhVienPhong>();

    public virtual NvtPhong Phong { get; set; } = null!;
}
