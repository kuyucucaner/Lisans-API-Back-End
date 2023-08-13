

using LisansAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

namespace LisansAPI.Controllers
{
    [AllowAnonymous]
    [EnableCors(origins: "*",  methods: "*", headers: "*")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerBranchController : ControllerBase
    {

            private readonly ercesa_terminalContext _context;

            public CustomerBranchController(ercesa_terminalContext context)
            {
                _context = context;
            }

            [HttpGet("CustomerBranchList")]
        public IEnumerable<CustomerBranch> GetCustomerBranchs()
            {
            try
                 {
                return _context.CustomerBranches.Where(x => x.IsDeleted == false).ToList();
                 }
            catch (Exception ee)
             {
                throw;
             }
             }
        [HttpGet("CustomerBranchListTata")]
        public IEnumerable<CustomerBranch> GetCustomerBranchTatas()
        {
            try
            {
                return _context.CustomerBranches.Include(x=>x.Customer).Where(x => x.IsDeleted == false).OrderByDescending(x=>x.CreateDate).ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        [HttpGet("CustomerBranchDetail")]
            public IActionResult GetCustomerBranch(int id)
            {

           CustomerBranch customerBranch = _context.CustomerBranches.Where(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
            if ( customerBranch == null)
                return NotFound("Müşteri Şubesi Bulunamadı.");
            return Ok(customerBranch);
        }
            [HttpPost("CreateCustomerBranch")]

        public IActionResult PostCustomerBranch(CustomerBranchDto customerBranch)
        {
                

            var CustomerBranch = new CustomerBranch()
            {
              
                CustomerId=customerBranch.CustomerId,
                Name=customerBranch.Name,
                Address=customerBranch.Address,
                ShortName=customerBranch.ShortName,
                CreateDate=customerBranch.CreateDate,
                BranchCloseTime=customerBranch.BranchCloseTime,
                IsDeleted = customerBranch.IsDeleted,
            };
                _context.CustomerBranches.Add(CustomerBranch);
                _context.SaveChanges();
            var ApiAddress = new ApiAddress()
            {
                CustomerBranchId = CustomerBranch.Id,
                Address = customerBranch.apiaddress,
                UserName = customerBranch.Name,
                Password = "Merkez",
                AddressType = (int)customerBranch.addresstype,
                IsDeleted = false,

            };
            if (ApiAddress.AddressType == 21)
            {
                ApiAddress.Address = "http://" + ApiAddress.Address + ':' + customerBranch.port;
                ApiAddress.UserName = "ercesa";
                ApiAddress.Password = "!narPOS!";
            }
            else if (ApiAddress.AddressType == 22)
            {
                ApiAddress.UserName = "ercesa";
                ApiAddress.Password = "ercesa";
                ApiAddress.Address = ApiAddress.Address + ':' + customerBranch.port;
            }
            _context.ApiAddresses.Add(ApiAddress);
            _context.SaveChangesAsync();

            return NoContent();

        }
        [HttpPost("UpdateCustomerBranch")]
            public IActionResult PostCustomerBranchs(int id, CustomerBranchDto customerBranch)
        {
  
            var CustomerBranch = new CustomerBranch()
            { Id = customerBranch.Id,
                CustomerId = customerBranch.CustomerId,
                Name = customerBranch.Name,
                Address = customerBranch.Address,
                ShortName = customerBranch.ShortName,
                CreateDate = customerBranch.CreateDate,
                BranchCloseTime = customerBranch.BranchCloseTime,
                IsDeleted=customerBranch.IsDeleted,

            };
            if (id != CustomerBranch.Id)
                {
                    return BadRequest();
                }

                _context.Entry(CustomerBranch).State = EntityState.Modified;

                try
                {
                    _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!CustomerBranchExists(id))
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
        [HttpPost("DeleteCustomerBranch")]
        public IActionResult PostCustomerBranch(int id)
        {
            var customerBranch = _context.CustomerBranches.Where(x => x.Id == id).SingleOrDefault();
            if (customerBranch == null)
            {
                return NotFound("Müşteri Şubesi Bulunamadı.");
            }
            customerBranch.IsDeleted = true;
            _context.SaveChanges();
            return NoContent();
        }

            private bool CustomerBranchExists(int id)
            {
                return (_context.CustomerBranches?.Any(e => e.Id == id)).GetValueOrDefault();
            }
        }
}
