using System;
using System.Collections.Generic;

namespace NvtHouseManagement.Models;

public partial class NvtChotDienNuoc
{
    public int Id { get; set; }

    public int PhongId { get; set; }

    public int Thang { get; set; }

    public int Nam { get; set; }

    public int SoDienCu { get; set; }

    public int SoDienMoi { get; set; }

    public int SoNuocCu { get; set; }

    public int SoNuocMoi { get; set; }

    public DateTime? NgayChot { get; set; }

    public virtual NvtPhong Phong { get; set; } = null!;
}
