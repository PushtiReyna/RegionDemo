using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Region.Models;

public partial class City
{
    public int CityId { get; set; }

    [Required(ErrorMessage = "Please Select Country.")]
    public int? CountryId { get; set; }

    [Required(ErrorMessage = "Please Select State.")]
    public int? StateId { get; set; }
    [Required(ErrorMessage = "Please Enter City Name"), MaxLength(50)]
   // [RegularExpression(@"^[a-zA-Z]+[a-zA-Z]$", ErrorMessage = "Please enter only letters for City Name.")]

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
