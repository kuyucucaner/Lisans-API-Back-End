using System;
using System.Collections.Generic;

namespace LisansAPI.Models
{
    public partial class User
    {
        public User()
        {
            Customers = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsGeneralAdmin { get; set; }
        public bool IsCustomerAdmin { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
