using LisansAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

namespace LisansAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [AllowAnonymous]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceLicenceController : ControllerBase
    {
        private readonly ercesa_terminalContext _context;

        public DeviceLicenceController(ercesa_terminalContext context)
        {
            _context = context;
        }
        [HttpGet("DeviceLicenceListTata")]
        public IEnumerable<DeviceLicence> GetDeviceLicencesTata()
        {
            try
            {
                return _context.DeviceLicences.Include(x => x.Device).ThenInclude(x => x.CustomerBranch).ThenInclude(x => x.Customer).OrderByDescending(x=>x.CreateDate).Take(500).Where(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        [HttpGet("DeviceLicenceList")]
        public IEnumerable<DeviceLicence> GetDeviceLicences()
        {
            try
            {                                                                                                                                                          //hepsini getirince çok çok çok yavaş geliyor. take500 s
                return _context.DeviceLicences.Where(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        [HttpGet("DeviceLicenceTodayList")]
        public IEnumerable<DeviceLicence> GetTodayDeviceLicences()
        {
            try
            {
                return _context.DeviceLicences.Where(x => x.IsDeleted == false && x.CreateDate == DateTime.Now.Date && x.LicenceStatus == 1).ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        [HttpGet("DeviceLicenceBossTodayConfirmList")]
        public IEnumerable<DeviceLicence> GetBossTodayConfirmDeviceLicences()
        {
            try
            {
                return _context.DeviceLicences.Where(x => x.IsDeleted == false && x.ConfirmedDate == DateTime.Now.Date && x.LicenceStatus == 2 && x.AppType== 1).ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        [HttpGet("DeviceLicenceTerminalTodayConfirmList")]
        public IEnumerable<DeviceLicence> GetTerminalTodayConfirmDeviceLicences()
        {
            try
            {
                return _context.DeviceLicences.Where(x => x.IsDeleted == false && x.ConfirmedDate == DateTime.Now.Date && x.LicenceStatus == 2 && x.AppType == 2).ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
      [HttpGet("DeviceLicenceBossWeekConfirmList")]
         public IEnumerable<DeviceLicence> GetBossWeekConfirmDeviceLicences()
         {
             DateTime sontarih = DateTime.Now.Date;
             DateTime  sontarih2 = DateTime.Now.Date.AddDays(-7);
            // TimeSpan sontarih3 = sontarih.Subtract(sontarih2);

            try
             {
                 return _context.DeviceLicences.Where(x => x.IsDeleted == false && (x.ConfirmedDate <= sontarih && x.ConfirmedDate >= sontarih2 ) && x.LicenceStatus == 2 && x.AppType == 1) .ToList();
             }
             catch (Exception ee)
             {
                 throw;
             }
         }
        [HttpGet("DeviceLicenceTerminalsWeekConfirmList")]
        public IEnumerable<DeviceLicence> GetTerminalWeekConfirmDeviceLicences()
        {
            DateTime sontarih = DateTime.Now.Date;
            DateTime sontarih2 = DateTime.Now.Date.AddDays(-7);
            // TimeSpan sontarih3 = sontarih.Subtract(sontarih2);

            try
            {
                return _context.DeviceLicences.Where(x => x.IsDeleted == false && (x.ConfirmedDate <= sontarih && x.ConfirmedDate >= sontarih2) && x.LicenceStatus == 2 && x.AppType == 2).ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        [HttpGet("DeviceLicenceBossMonthConfirmList")]
        public IEnumerable<DeviceLicence> GetBossMonthConfirmDeviceLicences()
        {
            DateTime sontarih = DateTime.Now.Date;
            DateTime sontarih2 = DateTime.Now.Date.AddMonths(-1);
            // TimeSpan sontarih3 = sontarih.Subtract(sontarih2);

            try
            {
                return _context.DeviceLicences.Where(x => x.IsDeleted == false && (x.ConfirmedDate <= sontarih && x.ConfirmedDate >= sontarih2) && x.LicenceStatus == 2 && x.AppType == 1).ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        [HttpGet("DeviceLicenceTerminalsMonthConfirmList")]
        public IEnumerable<DeviceLicence> GetTerminalMonthConfirmDeviceLicences()
        {
            DateTime sontarih = DateTime.Now.Date;
            DateTime sontarih2 = DateTime.Now.Date.AddMonths(-1);
            // TimeSpan sontarih3 = sontarih.Subtract(sontarih2);

            try
            {
                return _context.DeviceLicences.Where(x => x.IsDeleted == false && (x.ConfirmedDate <= sontarih && x.ConfirmedDate >= sontarih2) && x.LicenceStatus == 2 && x.AppType == 2).ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        [HttpGet("DeviceLicenceEndTime")]
        public IEnumerable<DeviceLicence> GetDeviceLicenceEndTime()
        {
            DateTime sontarih = DateTime.Now.Date;
            DateTime sontarih2 = DateTime.Now.Date.AddDays(7);
            // TimeSpan sontarih3 = sontarih.Subtract(sontarih2);

            try
            {
                return _context.DeviceLicences.Where(x => x.IsDeleted == false && (x.ExpireDate >= sontarih && x.ExpireDate <= sontarih2) && x.LicenceStatus == 2 && x.AppType == 2).ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        [HttpPost("AddTimeDeviceLicence")]
        public IActionResult PostAddTimeDeviceLicence(int id, DeviceLicenceDto devicelicence)
        {
                  
            var licence = _context.DeviceLicences.Where(x=>x.Id == id).SingleOrDefault();

            if (devicelicence == null)
            {
                return NotFound("Cihaz Lisansı Bulunamadı.");
            }
            licence.ExpireDate = devicelicence.ExpireDate;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("DeviceLicenceDetail")]
        public IActionResult GetDeviceLicence(int id)
        {
            DeviceLicence deviceLicence = _context.DeviceLicences.Where(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
            if (deviceLicence == null)
                return NotFound("Cihaz Lisansı Bulunamadı.");
            return Ok(deviceLicence);
        }
        [HttpPost("CreateDeviceLicence")]
        public IActionResult PostDeviceLicence(DeviceLicenceDto devicelicence)
        {
            var Devicelicence = new DeviceLicence()
            {
                DeviceId=devicelicence.DeviceId,
                LicenceStatus=devicelicence.LicenceStatus,
                AppType=devicelicence.AppType,
                CreateDate=devicelicence.CreateDate,
                ConfirmedDate=devicelicence.ConfirmedDate,
                ModifiedDate=devicelicence.ModifiedDate,
                ModifiedUserId=devicelicence.ModifiedUserId,
                ConfirmedUserId=devicelicence.ConfirmedUserId,
                DemanderUserId=devicelicence.DemanderUserId,
                ExpireDate=devicelicence.ExpireDate,
                IsDeleted = devicelicence.IsDeleted,
            };
            _context.DeviceLicences.Add(Devicelicence);
            _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDeviceLicences), new { id = Devicelicence.Id }, Devicelicence);
        }
        [HttpPost("UpdateDeviceLicence")]
        public IActionResult PostDeviceLicence(int id, DeviceLicenceDto devicelicence)
        {
            var Devicelicence = new DeviceLicence()
            {   Id=devicelicence.Id,
                DeviceId = devicelicence.DeviceId,
                LicenceStatus = devicelicence.LicenceStatus,
                AppType = devicelicence.AppType,
                CreateDate = devicelicence.CreateDate,
                ConfirmedDate = devicelicence.ConfirmedDate,
                ModifiedDate = devicelicence.ModifiedDate,
                ExpireDate = devicelicence.ExpireDate,
                IsDeleted = devicelicence.IsDeleted,

            };
            if (id != Devicelicence.Id)
            {
                return BadRequest();
            }

            _context.Entry(Devicelicence).State = EntityState.Modified;

            try
            {
               _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!DeviceLicenceExists(id))
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
        [HttpPost("DeleteDeviceLicence")]
        public IActionResult PostDeviceLicence(int id)
        {
            var devicelicence = _context.DeviceLicences.Where(x => x.Id == id).SingleOrDefault();
            if (devicelicence == null)
            {
                return NotFound("Cihaz Lisansı Bulunamadı.");
            }
            devicelicence.IsDeleted = true;
            _context.SaveChanges();
            return NoContent();
        }
        [HttpGet("DeviceLicenceActive")]
        public IEnumerable<DeviceLicence> GetDeviceLicenceActives()
        {
            try
            {
                return _context.DeviceLicences.Include(x=>x.Device).ThenInclude(x=>x.CustomerBranch).ThenInclude(x=>x.Customer).OrderByDescending(x => x.CreateDate).Where(x => x.LicenceStatus == 1).OrderByDescending(x => x.CreateDate).ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        [HttpPost("DeviceLicenceDoActive")]
        public IActionResult PostDeviceLicenceActive(int id)
        {
            var devicelicence = _context.DeviceLicences.Where(x => x.Id == id).SingleOrDefault();
            if (devicelicence == null)
            {
                return NotFound("Cihaz Lisansı Bulunamadı.");
            }
            devicelicence.LicenceStatus = 2;
            devicelicence.ConfirmedDate = DateTime.Now.Date;
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPost("DeviceLicenceNotActive")]
        public IActionResult PostDeviceLicenceNotActive(int id)
        {
            var devicelicence = _context.DeviceLicences.Where(x => x.Id == id).SingleOrDefault();
            if (devicelicence == null)
            {
                return NotFound("Cihaz Lisansı Bulunamadı.");
            }
            devicelicence.LicenceStatus = 1;
            _context.SaveChanges();
            return NoContent();
        }
        private bool DeviceLicenceExists(int id)
        {
            return (_context.DeviceLicences?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
