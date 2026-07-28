using System;
using System.Collections.Generic;

namespace NvtPhoneStore.Models;

public partial class NvtProduct
{
    public int NvtProductId { get; set; }

    public string NvtProductName { get; set; } = null!;

    public string NvtCategory { get; set; } = null!;

    public decimal NvtPrice { get; set; }

    public int NvtQuantity { get; set; }

    public string? NvtImage { get; set; }

    public string? NvtDescription { get; set; }

    public bool NvtIsActive { get; set; }
}
