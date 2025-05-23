﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergy.Models
{
    public class AddFarmerViewModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Cell Phone")]
        public string CellPhone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

   
        [Required]
        public string Region { get; set; }
    }
}
