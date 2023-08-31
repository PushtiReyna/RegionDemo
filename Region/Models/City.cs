using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Region.Models;

public partial class City
{
    public int CityId { get; set; }

    public int CountryId { get; set; }

    public int StateId { get; set; }

    public string CityName { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public DateTime CreateOn { get; set; }

    public DateTime? UpdateOn { get; set; }


    [NotMapped]
    public string CountryName { get; set; }

    [NotMapped]
    public string StateName { get; set; }
}
