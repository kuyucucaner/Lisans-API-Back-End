using System;
using System.Collections.Generic;

namespace LisansAPI.Models
{
    public partial class SystemUser
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual SystemUserRole? Role { get; set; }
    }
}
