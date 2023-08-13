using LisansAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

namespace LisansAPI.Controllers
{
    [AllowAnonymous]
    [EnableCors(origins: "*", methods: "*", headers: "*")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ercesa_terminalContext _context;

        public UserController(ercesa_terminalContext context)
        {
            _context = context;
        }
   
        [HttpGet("UsersList")]

        public IEnumerable<User> GetUsers()
        {
            DateTime sontarih = DateTime.Now.Date;
            try
            {
                return _context.Users.Where(x => x.IsDeleted == false && x.ExpireDate >= sontarih).OrderBy(x=> x.ExpireDate).ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        [HttpGet("UserDetail")]
        public IActionResult GetUser(int id)
        {
            User User = _context.Users.Where(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
            if (User == null)
                return NotFound("Kullanıcı Bulunamadı.");
            return Ok(User);
        }
        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool UserNameExists(string username)
        {
            return (_context.Users?.Any(e => e.UserName == username)).GetValueOrDefault();
        }
        [HttpPost("CreateUsers")]
        public IActionResult PostUser(UserDto user)
        {
         //   TimeSpan timeNow = DateTime.Now.TimeOfDay;
         //   TimeSpan trimmedTimeNow = new TimeSpan(timeNow.Hours, timeNow.Minutes, timeNow.Seconds);
            var User = new User()
            {
                Name = user.Name,
                LastName=user.LastName,
                UserName=user.UserName,
                Email=user.Email,
                Password=user.Password,
                IsGeneralAdmin=user.IsGeneralAdmin,
                IsCustomerAdmin=user.IsCustomerAdmin,
                CreateDate=user.CreateDate,
                ExpireDate=user.ExpireDate,
                IsDeleted = user.IsDeleted,
            };
            if (!UserNameExists(User.UserName))
            {
                _context.Users.Add(User);
                _context.SaveChanges();
               
            
          

            var Customers = new Customer()
            {
                Name = user.Name,
                ShortName = "Merkez34",
                Alias = "Merkez",
                Address = "Merkez",
                TaxNumber = "Merkez",
                TaxOffice = "Merkez",
                CreateDate = DateTime.Now,
                UserId = User.Id,
                IsDeleted = false,
            };
            _context.Customers.Add(Customers);
            _context.SaveChanges();
            var CustomerBranch = new CustomerBranch()
            {
                CustomerId = Customers.Id,
                Name =user.Name,
                Address = "Merkez34",
                ShortName = "Merkez",
                CreateDate = DateTime.Now,
                BranchCloseTime = (TimeSpan)user.branchclosetime,
                IsDeleted = false,
            };
            _context.CustomerBranches.Add(CustomerBranch);
            _context.SaveChanges();
            var ApiAddress = new ApiAddress()
            {
                CustomerBranchId = CustomerBranch.Id,
                Address = user.apiaddress,
                UserName = user.UserName,
                Password = user.Password,
                AddressType = (int)user.addresstype,
                IsDeleted = false,

            };
            if (ApiAddress.AddressType == 21)
            {
                ApiAddress.Address = "http://" + ApiAddress.Address + ':' + user.port;
            }
            else if (ApiAddress.AddressType == 22)
            {
                ApiAddress.Address = ApiAddress.Address + ':'+ user.port;
            }
            _context.ApiAddresses.Add(ApiAddress);
            _context.SaveChangesAsync();
                return Ok(new { hata = true });
            }

            else
            {
                return Ok(new { hata = false });
            }
     
        }

        [HttpPost("UpdateUsers")]
        public IActionResult PostUser(int id, UserDto user)
        {
            var User = new User()
            {   Id=user.Id,
                Name = user.Name,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                IsGeneralAdmin = user.IsGeneralAdmin,
                IsCustomerAdmin = user.IsCustomerAdmin,
                CreateDate = user.CreateDate,
                ExpireDate = user.ExpireDate,
                IsDeleted = user.IsDeleted,

            };
            if (id != User.Id)
            {
                return BadRequest();
            }

            _context.Entry(User).State = EntityState.Modified;

            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!UserExists(id))
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
        [HttpPost("DeleteUser")]
        public IActionResult PostUser(int id)
        {
            var User = _context.Users.Where(x=>x.Id == id).SingleOrDefault();
            if (User == null)
            {
                return NotFound("Kullanıcı Bulunamadı.");
            }
            User.IsDeleted = true;
            _context.SaveChanges(); 
            return NoContent();
        }
     
    }
}
