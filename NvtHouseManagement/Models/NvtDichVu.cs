using System;
using System.Collections.Generic;

namespace NvtHouseManagement.Models;

public partial class NvtDichVu
{
    public int Id { get; set; }

    public string TenDichVu { get; set; } = null!;

    public decimal DonGia { get; set; }

    public string DonViTinh { get; set; } = null!;

    public virtual ICollection<NvtChiTietDichVu> NvtChiTietDichVus { get; set; } = new List<NvtChiTietDichVu>();
}
