using System;
using System.Collections.Generic;

namespace LisansAPI.Models
{
    public partial class SystemUserRole
    {
        public SystemUserRole()
        {
            SystemUsers = new HashSet<SystemUser>();
        }

        public int Id { get; set; }
        public int RoleType { get; set; }
        public string Role { get; set; } = null!;

        public virtual ICollection<SystemUser> SystemUsers { get; set; }
    }
}
