namespace LisansAPI.DTO
{
    public class SystemUserDto
    {   public int Id { get; set; }
        public int? RoleId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

        public DateTime? CreateDate { get; set; }
        public bool IsDeleted { get; set; }
    }   
}
