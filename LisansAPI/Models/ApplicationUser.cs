using System;
using System.Collections.Generic;

namespace LisansAPI.Models
{
    public partial class ApplicationUser
    {
        public int Id { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
    }
}
