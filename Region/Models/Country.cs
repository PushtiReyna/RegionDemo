using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Region.Models;

public partial class Country
{
    public int CountryId { get; set; }

    [Required(ErrorMessage = "Please Country Name"), MaxLength(50)]
    [RegularExpression(@"^[a-zA-Z]+[a-zA-Z]$", ErrorMessage = "Please enter only letters for Country Name.")]
    public string CountryName { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public DateTime CreateOn { get; set; }

    public DateTime? UpdateOn { get; set; }
}
