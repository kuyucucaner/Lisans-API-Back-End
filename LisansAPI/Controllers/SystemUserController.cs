using LisansAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LisansAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemUserController : ControllerBase
    {
        private readonly ercesa_terminalContext _context;

        public SystemUserController(ercesa_terminalContext context)
        {
            _context = context;
        }

        [HttpGet("SystemUserList")]

        public IEnumerable<SystemUser> GetSystemUsers()
        {
            try
            {
                return _context.SystemUsers.Where(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ee)
            {
                throw;
            }
        }
        [HttpGet("SystemUserDetail")]
        public IActionResult GetSystemUsers(int id)
        {
            SystemUser systemUser = _context.SystemUsers.Where(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
            if (systemUser == null)
                return NotFound("Sistem Kullanıcısı Bulunamadı.");
            return Ok(systemUser);
        }

        private bool SystemUserExists(string username)
        {
            return (_context.SystemUsers?.Any(e => e.UserName == username)).GetValueOrDefault();
        }
        [HttpPost("CreateSystemUsers")]
        public IActionResult PostUser(SystemUserDto systemuser)
        {  
            var systemUser = new SystemUser()
            {   RoleId = systemuser.RoleId,
                UserName = systemuser.UserName,
                Password = systemuser.Password,
                CreateDate = systemuser.CreateDate,
                IsDeleted = systemuser.IsDeleted,
            };
            if (!SystemUserExists(systemUser.UserName))
            {
                _context.SystemUsers.Add(systemUser);
                 _context.SaveChangesAsync();
                return Ok(new { hata = true });
            }
             else  {
                return Ok(new { hata = false });
            }
            return NoContent();
        }
        private bool SystemUsersExists2(int id)
        {
            return (_context.SystemUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [HttpPost("UpdateSystemUsers")]
        public IActionResult PostCustomer(int id, SystemUserDto systemuser)
        {
            var systemUser = new SystemUser()
            {   Id = systemuser.Id,
                RoleId = systemuser.RoleId,
                UserName = systemuser.UserName,
                Password = systemuser.Password,
                CreateDate = systemuser.CreateDate,
                IsDeleted = systemuser.IsDeleted,
            };
            if (id != systemUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(systemUser).State = EntityState.Modified;

            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!SystemUsersExists2(id))
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
        [HttpPost("DeleteSystemUser")]
        public IActionResult PostUser(int id)
        {
            var systemUser = _context.SystemUsers.Where(x => x.Id == id).SingleOrDefault();
            if (systemUser == null)
            {
                return NotFound("Sistem Kullanıcısı Bulunamadı.");
            }
            systemUser.IsDeleted = true;
            _context.SaveChanges();
            return NoContent();
        }

    }
}
