using System;
using System.Collections.Generic;

namespace NvtHouseManagement.Models;

public partial class NvtKhachThue
{
    public int Id { get; set; }

    public string HoTen { get; set; } = null!;

    public string SoDienThoai { get; set; } = null!;

    public string Cccd { get; set; } = null!;

    public string? Email { get; set; }

    public string? QueQuan { get; set; }

    public string? UserId { get; set; }

    public virtual ICollection<NvtBaoCaoSuCo> NvtBaoCaoSuCos { get; set; } = new List<NvtBaoCaoSuCo>();

    public virtual ICollection<NvtHopDong> NvtHopDongs { get; set; } = new List<NvtHopDong>();
}
