using System.ComponentModel.DataAnnotations;

namespace LisansAPI.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? UserName { get; set; }
        
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public bool IsGeneralAdmin { get; set; }
        [Required]
        public bool IsCustomerAdmin { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime ExpireDate { get; set; }

        public bool IsDeleted { get; set; } = false;

        public TimeSpan? branchclosetime { get; set; } = null;

        public int? addresstype { get; set; } = null;

        public string? apiaddress { get; set; } = null;

        public string? port { get; set; } = null;
    }
}
