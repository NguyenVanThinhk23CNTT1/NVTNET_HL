using System;
using System.Collections.Generic;

namespace NvtHouseManagement.Models;

public partial class NvtDayTro
{
    public int Id { get; set; }

    public string TenDayTro { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string? MoTa { get; set; }

    public virtual ICollection<NvtPhong> NvtPhongs { get; set; } = new List<NvtPhong>();
}
