using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Region.Models;

public partial class State
{
    public int StateId { get; set; }
  
    [Required, Range(1,int.MaxValue,ErrorMessage = "Please Select Country.")]
    public int CountryId { get; set; }

    [Required(ErrorMessage = "Please Enter State Name"), MaxLength(50)]
    [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "Please enter only letters for State Name.")]
    public string StateName { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public DateTime CreateOn { get; set; }

    public DateTime? UpdateOn { get; set; }

    [NotMapped]
    public string CountryName { get; set; }

}
