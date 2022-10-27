using KJCMediaChannelWebAPI.Data;
using KJCMediaChannelWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KJCMediaChannelWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly APIDbContext dbContext;

        public RegistrationController(APIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //* GET Methods *//
        [HttpGet]
        public async Task<ActionResult<Registration>> getRegistrations()
        {
            return Ok(await dbContext.Registeration.ToListAsync());
        }

        [HttpGet("{eventId:guid}")]
        public async Task<ActionResult<Registration>> getRegistrationEvent(Guid eventId)
        {
            var registration = dbContext.Registeration.Where(register => register.EventId == eventId);
            if (registration != null)
            {
                return Ok(registration);
            }
            return NotFound("registrations for that event not found!");
        }

        [HttpGet("{eventId:guid}/{username}")]
        public async Task<ActionResult<Registration>> getRegistrationEventUser(Guid eventId, string username)
        {
            var registration = await dbContext.Registeration.FirstOrDefaultAsync(register => register.EventId == eventId && register.Username == username);
            if (registration != null)
            {
                return Ok("Found");
            } else
            {
                return Ok("Not Found");
            }
        }

        //* POST Methods *//
        [HttpPost]
        public async Task<ActionResult<Registration>> addRegistration(RegistrationRequest registrationRequest)
        {
            var registration = new Registration()
            {
                Id = Guid.NewGuid(),
                EventId = registrationRequest.EventId,
                RegistrationTime = DateTime.Now,
                Regno = registrationRequest.Regno,
                Username = registrationRequest.Username,
                Department = registrationRequest.Department,
            };

            await dbContext.Registeration.AddAsync(registration);
            await dbContext.SaveChangesAsync();

            return Ok(registration);
        }

        //* DELETE Methods *//
        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult<Registration>> deleteRegistration(Guid Id)
        {
            var registration = await dbContext.Registeration.FindAsync(Id);
            if (registration != null)
            {
                dbContext.Registeration.Remove(registration);
                await dbContext.SaveChangesAsync();

                return Ok(registration);
            }
            return NotFound("Registration not found!");
        }
    }
}
