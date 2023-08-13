using LisansAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

namespace LisansAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [AllowAnonymous]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ercesa_terminalContext _context;

        public CustomerController(ercesa_terminalContext context)
        {
            _context = context;
        }
        [HttpGet("CustomersList")]

        public IEnumerable<Customer> GetCustomers()
        {
            try
            {
                return _context.Customers.Where(x => x.IsDeleted == false).OrderByDescending(x=>x.CreateDate).ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        [HttpGet("CustomersDetail")]
        public IActionResult GetCustomer(int id)
        {
            Customer customer = _context.Customers.Where(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
            if (customer == null)
                return NotFound("Müşteri Bulunamadı.");
            return Ok(customer);
        }
        [HttpPost("CreateCustomers")]
        public IActionResult PostCustomer(CustomerDto customer)
        {
            TimeSpan timeNow = DateTime.Now.TimeOfDay;
            TimeSpan closeTime = new TimeSpan(timeNow.Hours, timeNow.Minutes, timeNow.Seconds);
            var Customers = new Customer()
            {

                Name = customer.Name,
                ShortName = customer.ShortName,
                Alias = customer.Alias,
                Address = customer.Address,
                TaxNumber = customer.TaxNumber,
                TaxOffice = customer.TaxOffice,
                CreateDate = customer.CreateDate,
                UserId = customer.UserId,
                IsDeleted = customer.IsDeleted,
            };
            _context.Customers.Add(Customers);
            _context.SaveChanges();
            var CustomerBranch = new CustomerBranch()
            {
                CustomerId = Customers.Id,
                Name = "Merkez",
                Address = "Merkez",
                ShortName = "Merkez",
                CreateDate = DateTime.Now,
                BranchCloseTime = closeTime,
                IsDeleted = false,
            };
            _context.CustomerBranches.Add(CustomerBranch);
            _context.SaveChanges();
            var ApiAddress = new ApiAddress()
            {
                CustomerBranchId = CustomerBranch.Id,
                Address = "merkez",
                UserName = "Merkez",
                Password = "Merkez",
                AddressType = 21,
                IsDeleted = false,

            };
            if (ApiAddress.AddressType == 21)
            {
                ApiAddress.Address = "http://" + ApiAddress.Address;
            }
            else if (ApiAddress.AddressType == 22)
            {
                ApiAddress.Address = ApiAddress.Address + ":9001";
            }
            _context.ApiAddresses.Add(ApiAddress);
            _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost("UpdateCustomers")]
        public IActionResult PostCustomer(int id, CustomerDto customer)
        {
            var Customers = new Customer()
            {   Id=customer.Id,
                Name = customer.Name,
                ShortName = customer.ShortName,
                Alias = customer.Alias,
                Address = customer.Address,
                TaxNumber = customer.TaxNumber,
                TaxOffice = customer.TaxOffice,
                CreateDate = customer.CreateDate,
                UserId = customer.UserId,
                IsDeleted = customer.IsDeleted,

            };
            if (id != Customers.Id)
            {
                return BadRequest();
            }

            _context.Entry(Customers).State = EntityState.Modified;

            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPost("DeleteCustomers")]
        public IActionResult PostCustomer(int id)
        {
            var customer = _context.Customers.Where(x => x.Id == id).SingleOrDefault();
            if (customer == null)
            {
                return NotFound("Müşteri Bulunamadı.");
            }
            customer.IsDeleted = true;
            _context.SaveChanges();
            return NoContent();
        }
        private bool CustomerExists(int id)
        {
            return (_context.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

