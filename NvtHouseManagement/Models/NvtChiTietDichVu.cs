using System;
using System.Collections.Generic;

namespace NvtHouseManagement.Models;

public partial class NvtChiTietDichVu
{
    public int Id { get; set; }

    public int HopDongId { get; set; }

    public int DichVuId { get; set; }

    public int SoLuong { get; set; }

    public virtual NvtDichVu DichVu { get; set; } = null!;

    public virtual NvtHopDong HopDong { get; set; } = null!;
}
