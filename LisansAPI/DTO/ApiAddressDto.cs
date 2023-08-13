using System.ComponentModel.DataAnnotations;

namespace LisansAPI.DTO
{
    public class ApiAddressDto
    {
        public int Id { get; set; }

        [Required]
        public int CustomerBranchId { get; set; }
        [Required]
        public string? Address { get; set; }
     
        public string? UserName { get; set; }

        public string? Password { get; set; }
        [Required]
        public int AddressType { get; set; }

        public string port { get; set; }

        public bool IsDeleted { get; set; } = false;




    }
}
