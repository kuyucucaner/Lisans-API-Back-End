using System.ComponentModel.DataAnnotations;

namespace LisansAPI.DTO
{
    public class DeviceLicenceDto
    {
        public int Id { get; set; }
        [Required]
        public int DeviceId { get; set; }
        [Required]
        public int LicenceStatus { get; set; }
        [Required]
        public int AppType { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime ConfirmedDate { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }

        [Required]
        public int ModifiedUserId { get; set; }
        [Required]
        public int ConfirmedUserId { get; set; }
        [Required]
        public int DemanderUserId { get; set; }
        [Required]
        public DateTime ExpireDate { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
