using System.Text.Json.Serialization;

namespace LisansAPI.DTO
{
    public class CustomerBranchDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? ShortName { get; set; }
        public DateTime CreateDate { get; set; }
        public TimeSpan BranchCloseTime { get; set; }
        public bool? IsDeleted { get; set; } = false;

        public int? addresstype { get; set; } = null;

        public string? apiaddress { get; set; } = null;

        public string? port { get; set; } = null;

    }
}
