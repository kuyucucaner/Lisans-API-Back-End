using System;
using System.Collections.Generic;

namespace LisansAPI.Models
{
    public partial class CustomerBranch
    {
        public CustomerBranch()
        {
            ApiAddresses = new HashSet<ApiAddress>();
            Devices = new HashSet<Device>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? ShortName { get; set; }
        public DateTime CreateDate { get; set; }
        public TimeSpan BranchCloseTime { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<ApiAddress> ApiAddresses { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
    }
}
