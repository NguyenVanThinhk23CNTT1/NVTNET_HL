using System;
using System.Collections.Generic;

namespace NvtHouseManagement.Models;

public partial class NvtLoaiPhong
{
    public int Id { get; set; }

    public string TenLoaiPhong { get; set; } = null!;

    public decimal GiaCoBan { get; set; }

    public int SoNguoiToiDa { get; set; }

    public string? MoTa { get; set; }

    public virtual ICollection<NvtPhong> NvtPhongs { get; set; } = new List<NvtPhong>();
}
