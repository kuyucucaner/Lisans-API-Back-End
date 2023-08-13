using System;
using System.Collections.Generic;

namespace LisansAPI.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerBranches = new HashSet<CustomerBranch>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public string? Alias { get; set; }
        public string? Address { get; set; }
        public string? TaxNumber { get; set; }
        public string? TaxOffice { get; set; }
        public DateTime CreateDate { get; set; }
        public int? UserId { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<CustomerBranch> CustomerBranches { get; set; }
    }
}
