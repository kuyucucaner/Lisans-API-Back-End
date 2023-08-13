using System;
using System.Collections.Generic;

namespace LisansAPI.Models
{
    public partial class ApiAddress
    {
        public int Id { get; set; }
        public int CustomerBranchId { get; set; }
        public string? Address { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int AddressType { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual CustomerBranch CustomerBranch { get; set; } = null!;
    }
}
