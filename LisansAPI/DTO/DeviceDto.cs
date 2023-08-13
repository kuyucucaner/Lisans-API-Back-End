using System.ComponentModel.DataAnnotations;

namespace LisansAPI.DTO
{
    public class DeviceDto
    {
        public int Id { get; set; }
        [Required]
        public int CustomerBranchId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Model { get; set; }
        [Required]
        public string? Platform { get; set; }
        [Required]
        public string? Uuid { get; set; }
        [Required]
        public string? Version { get; set; }
        [Required]
        public string? Manufacturer { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
