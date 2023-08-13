using System.ComponentModel.DataAnnotations;

namespace LisansAPI.DTO
{
    public class CustomerDto
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? ShortName { get; set; }
        [Required]
        public string? Alias { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? TaxNumber { get; set; }
        [Required]
        public string? TaxOffice { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public int? UserId { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
