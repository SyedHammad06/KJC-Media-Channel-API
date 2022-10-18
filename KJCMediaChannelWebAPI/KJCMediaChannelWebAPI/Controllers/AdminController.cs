using KJCMediaChannelWebAPI.Data;
using KJCMediaChannelWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KJCMediaChannelWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly APIDbContext dbContext;

        public AdminController(APIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //* GET Methods *//
        [HttpGet]
        public async Task<ActionResult<Admin>> GetAdmins()
        {
            return Ok(await dbContext.Admins.ToListAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Admin>> GetAdmin(Guid id)
        {
            var admin = await dbContext.Admins.FindAsync(id);
            if (admin != null)
            {
                return Ok(admin);
            }
            return NotFound("Admin Not Found!");
        }

        //* POST Methods *//
        [HttpPost]
        public async Task<ActionResult<Admin>> AddAdmin(AdminRequest adminRequest)
        {
            var admin = new Admin()
            {
                Id = Guid.NewGuid(),
                Username = adminRequest.Username,
                Email = adminRequest.Email,
                Designation = adminRequest.Designation,
                PhoneNo = adminRequest.PhoneNo,
                Password = adminRequest.Password,
                Department = adminRequest.Department
            };
            await dbContext.Admins.AddAsync(admin);
            await dbContext.SaveChangesAsync();

            return Ok(admin);
        }

        //* PUT Methods *//
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Admin>> UpdateAdmin(Guid id, AdminRequest adminRequest)
        {
            var admin = await dbContext.Admins.FindAsync(id);
            if (admin != null)
            {
                admin.Username = adminRequest.Username;
                admin.Password = adminRequest.Password;
                admin.PhoneNo = adminRequest.PhoneNo;
                admin.Email = adminRequest.Email;
                admin.Designation = adminRequest.Designation;
                admin.Department = adminRequest.Department;

                await dbContext.SaveChangesAsync();

                return Ok(admin);
            }
            return NotFound("Admin not found!");
        }

        //* DELETE Methods *//
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Admin>> DeleteAdmin(Guid id)
        {
            var admin = await dbContext.Admins.FindAsync(id);
            if (admin != null)
            {
                dbContext.Admins.Remove(admin);
                await dbContext.SaveChangesAsync();

                return Ok("Admin Removed Succesfully!");
            }
            return NotFound("Admin not found!");
        }
    }
}
