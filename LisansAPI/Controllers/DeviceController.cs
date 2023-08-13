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
    public class DeviceController : ControllerBase
    {
        private readonly ercesa_terminalContext _context;

        public DeviceController(ercesa_terminalContext context)
        {
            _context = context;
        }
        [HttpGet("DeviceList")]
        public IEnumerable<Device> GetDevices()
        {
            try
            {
                return _context.Devices.Where(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        [HttpGet("DeviceListTata")]
        public IEnumerable<Device> GetDevicesTata()
        {
            try
            {
                return _context.Devices.Include(x => x.CustomerBranch).ThenInclude(x => x.Customer).OrderByDescending(x=>x.CreateDate).Take(500).Where(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        [HttpGet("DeviceDetail")]
        public IActionResult GetDevice(int id)
        {
            Device device = _context.Devices.Where(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
            if (device == null)
                return NotFound("Cihaz Bulunamadı.");
            return Ok(device);
        }
        [HttpPost("CreateDevice")]
        public IActionResult PostDevice(DeviceDto device)
        {
            var Device = new Device()
            {
                CustomerBranchId = device.CustomerBranchId,
                Name=device.Name,
                Model=device.Model,
                Platform=device.Platform,
                Uuid=device.Uuid,
                Version=device.Version,
                Manufacturer=device.Manufacturer,
                CreateDate=device.CreateDate,
                 IsDeleted =device.IsDeleted,


            };
            _context.Devices.Add(Device);
            _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDevices), new { id = Device.Id }, Device);
        }
        [HttpPost("UpdateDevice")]
        public IActionResult PostDevice(int id,DeviceDto device)
        {
            var Device = new Device()
            {   Id=device.Id,
                CustomerBranchId = device.CustomerBranchId,
                Name = device.Name,
                Model = device.Model,
                Platform = device.Platform,
                Uuid = device.Uuid,
                Version = device.Version,
                Manufacturer = device.Manufacturer,
                CreateDate = device.CreateDate,
                   IsDeleted = device.IsDeleted,

            };
            if (id != Device.Id)
            {
                return BadRequest();
            }

            _context.Entry(Device).State = EntityState.Modified;

            try
            {
               _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!DeviceExists(id))
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
        [HttpPost("DeleteDevice")]
        public IActionResult PostDevice(int id)
        {
            var device = _context.Devices.Where(x => x.Id == id).SingleOrDefault();
            if (device == null)
            {
                return NotFound("Cihaz Bulunamadı.");
            }
            device.IsDeleted = true;
            _context.SaveChanges();
            return NoContent();
        }
        private bool DeviceExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
