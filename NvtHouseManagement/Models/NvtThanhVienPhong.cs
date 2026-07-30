using System;
using System.Collections.Generic;

namespace NvtHouseManagement.Models;

public partial class NvtThanhVienPhong
{
    public int Id { get; set; }

    public int HopDongId { get; set; }

    public string HoTen { get; set; } = null!;

    public string? Cccd { get; set; }

    public string? SoDienThoai { get; set; }

    public string? QuanHeVoiChuHo { get; set; }

    public virtual NvtHopDong HopDong { get; set; } = null!;
}
