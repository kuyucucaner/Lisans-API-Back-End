using System;
using System.Collections.Generic;

namespace LisansAPI.Models
{
    public partial class JWTToken
    {
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }
        public string? RefreshToken { get; set; }
    }
}
