using LisansAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Http.Cors;

namespace LisansAPI.Controllers
{
    [AllowAnonymous]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiAddressController : ControllerBase
    {
        private readonly ercesa_terminalContext _context;

        public ApiAddressController(ercesa_terminalContext context)
        {
            _context = context;
        }
        [HttpGet("ApiAddressList")]
        public IEnumerable<ApiAddress> GetApiAddresses()
        {
            try
            {
                return _context.ApiAddresses.Include(a => a.CustomerBranch).ThenInclude(a => a.Customer).Where(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        [HttpGet("ApiAddressDetail")]
        
        public IActionResult GetApiAddress(int id)
        {
            ApiAddress apiAddress = _context.ApiAddresses.Where(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
            if (apiAddress == null)
                return NotFound("Api Adres Bulunamadı.");
            return Ok(apiAddress);
        }
        [HttpPost("CreateApiAddress")]
        public IActionResult PostApiAddress(ApiAddressDto ApiAdres)
        {
            var ApiAddress = new ApiAddress()
            {  
            CustomerBranchId = ApiAdres.CustomerBranchId,
                Address = ApiAdres.Address,
                UserName = ApiAdres.UserName,
                Password = ApiAdres.Password,
                AddressType = ApiAdres.AddressType,
                IsDeleted = ApiAdres.IsDeleted,


            };
            if(ApiAddress.AddressType == 21)
            {
                ApiAddress.Address = "http://" + ApiAddress.Address + ":" + ApiAdres.port;
                ApiAddress.UserName = "ercesa";
                ApiAddress.Password = "!narPOS!";
            }
            else if (ApiAddress.AddressType == 22)
            {
                ApiAddress.UserName = "ercesa";
                ApiAddress.Password = "ercesa";
                ApiAddress.Address = ApiAddress.Address + ":" + ApiAdres.port;
            }
            else if (ApiAddress.AddressType == 23)
            {
                ApiAddress.UserName = "ercesa";
                ApiAddress.Password = "ercesa";
                ApiAddress.Address = ApiAddress.Address + ":" + ApiAdres.port;
            }

            _context.ApiAddresses.Add(ApiAddress);
            _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetApiAddress), new { id = ApiAddress.Id },ApiAddress);
        }
        [HttpPost("UpdateApiAddress")]
     
        public IActionResult PostApiAddress(int id, ApiAddressDto ApiAdres)
        {
            var ApiAddress = new ApiAddress()
            {
               Id = ApiAdres.Id,
                CustomerBranchId = ApiAdres.CustomerBranchId,
                Address = ApiAdres.Address,
                UserName = ApiAdres.UserName,
                Password = ApiAdres.Password,
                AddressType = ApiAdres.AddressType,
                IsDeleted = ApiAdres.IsDeleted,


            };
            if (id != ApiAddress.Id)
            {
                return BadRequest();
            }
        
            _context.Entry(ApiAddress).State = EntityState.Modified;

            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!ApiAddressExists(id))
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
        [HttpPost("DeleteApiAddress")]
        public IActionResult PostApiAddress(int id)
        {
            var apiAdres = _context.ApiAddresses.Where(x => x.Id == id).SingleOrDefault();
            if (apiAdres == null)
            {
                return NotFound("Kullanıcı Bulunamadı.");
            }
            apiAdres.IsDeleted = true;
            _context.SaveChanges();
            return NoContent();
        }
        private bool ApiAddressExists(int id)
        {
            return (_context.ApiAddresses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

    





