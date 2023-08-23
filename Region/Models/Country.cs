using System;
using System.Collections.Generic;

namespace Region.Models;

public partial class Country
{
    public int CountryId { get; set; }

    public string CountryName { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public DateTime CreateOn { get; set; }

    public DateTime UpdateOn { get; set; }
}
