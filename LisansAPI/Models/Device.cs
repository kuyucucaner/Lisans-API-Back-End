using System;
using System.Collections.Generic;

namespace LisansAPI.Models
{
    public partial class Device
    {
        public Device()
        {
            DeviceLicences = new HashSet<DeviceLicence>();
        }

        public int Id { get; set; }
        public int CustomerBranchId { get; set; }
        public string? Name { get; set; }
        public string? Model { get; set; }
        public string? Platform { get; set; }
        public string? Uuid { get; set; }
        public string? Version { get; set; }
        public string? Manufacturer { get; set; }
        public DateTime CreateDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual CustomerBranch CustomerBranch { get; set; } = null!;
        public virtual ICollection<DeviceLicence> DeviceLicences { get; set; }
    }
}
