﻿using Microsoft.AspNetCore.Identity;

namespace AgriEnergy.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CellPhone { get; set; }

        public string? Region { get; set; }
    }
}
