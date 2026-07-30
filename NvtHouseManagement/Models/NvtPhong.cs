using System;
using System.Collections.Generic;

namespace NvtHouseManagement.Models;

public partial class NvtPhong
{
    public int Id { get; set; }

    public string SoPhong { get; set; } = null!;

    public int Tang { get; set; }

    public string TrangThai { get; set; } = null!;

    public int DayTroId { get; set; }

    public int LoaiPhongId { get; set; }

    public virtual NvtDayTro DayTro { get; set; } = null!;

    public virtual NvtLoaiPhong LoaiPhong { get; set; } = null!;

    public virtual ICollection<NvtBaoCaoSuCo> NvtBaoCaoSuCos { get; set; } = new List<NvtBaoCaoSuCo>();

    public virtual ICollection<NvtChotDienNuoc> NvtChotDienNuocs { get; set; } = new List<NvtChotDienNuoc>();

    public virtual ICollection<NvtHopDong> NvtHopDongs { get; set; } = new List<NvtHopDong>();
}
