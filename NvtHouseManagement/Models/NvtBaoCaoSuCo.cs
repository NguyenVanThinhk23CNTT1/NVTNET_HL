using System;
using System.Collections.Generic;

namespace NvtHouseManagement.Models;

public partial class NvtBaoCaoSuCo
{
    public int Id { get; set; }

    public int PhongId { get; set; }

    public int KhachThueId { get; set; }

    public string TieuDe { get; set; } = null!;

    public string NoiDung { get; set; } = null!;

    public string TrangThai { get; set; } = null!;

    public DateTime? NgayTao { get; set; }

    public virtual NvtKhachThue KhachThue { get; set; } = null!;

    public virtual NvtPhong Phong { get; set; } = null!;
}
