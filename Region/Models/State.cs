using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Region.Models;

public partial class State
{
    public int StateId { get; set; }
  
    [Required, Range(1,int.MaxValue,ErrorMessage = "Please Select Country.")]
    [RegularExpression(@"^[a-zA-Z]+[a-zA-Z]$", ErrorMessage = "Please enter only letters for Country Name.")]
   // ^[a-zA-Z][a-zA-Z\\s]+$
    public int CountryId { get; set; }

    [Required(ErrorMessage = "Please Enter State Name"), MaxLength(50)]
    public string StateName { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public DateTime CreateOn { get; set; }

    public DateTime? UpdateOn { get; set; }

    [NotMapped]
    public string CountryName { get; set; }

}
