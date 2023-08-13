using System;
using System.Collections.Generic;

namespace LisansAPI.Models
{
    public partial class DeviceLicence
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int LicenceStatus { get; set; }
        public int AppType { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ConfirmedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedUserId { get; set; }
        public int ConfirmedUserId { get; set; }
        public int DemanderUserId { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Device Device { get; set; } = null!;
    }
}
