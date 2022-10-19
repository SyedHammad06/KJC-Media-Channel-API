using KJCMediaChannelWebAPI.Data;
using KJCMediaChannelWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KJCMediaChannelWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly APIDbContext dbContext;

        public EventController(APIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //* GET Methods *//
        [HttpGet]
        public async Task<ActionResult<Event>> getEvents()
        {
            return Ok(await dbContext.Events.ToListAsync());
        }

        [HttpGet("{Id:guid}")]
        public async Task<ActionResult<Event>> getEvent(Guid Id)
        {
            var eventDetails = await dbContext.Events.FindAsync(Id);
            if (eventDetails != null)
            {
                return Ok(eventDetails);
            }
            return NotFound("Event not found!");
        }

        //* POST Methods *//
        [HttpPost]
        public async Task<ActionResult<Event>> addEvent(EventRequest eventRequest)
        {
            var eventDetails = new Event()
            {
                Id = Guid.NewGuid(),
                UserId = eventRequest.UserId,
                Description = eventRequest.Description,
                Title = eventRequest.Title,
                Department = eventRequest.Department,
                MaxSlots = eventRequest.MaxSlots,
                ImageLocation = eventRequest.ImageLocation,
                CurrentSlots = 0,
            };

            await dbContext.Events.AddAsync(eventDetails);
            await dbContext.SaveChangesAsync();

            return Ok(eventDetails);
        }

        //* PUT Methods *//
        [HttpPut("{Id:guid}")]
        public async Task<ActionResult<Event>> updateEvent(Guid Id, EventRequest eventRequest)
        {
            var eventDetails = await dbContext.Events.FindAsync(Id);
            if (eventDetails != null)
            {
                eventDetails.UserId = eventRequest.UserId;
                eventDetails.Title = eventRequest.Title;
                eventDetails.Description = eventRequest.Description;
                eventDetails.Department = eventRequest.Department;
                eventDetails.MaxSlots = eventRequest.MaxSlots;
                eventDetails.ImageLocation = eventRequest.ImageLocation;

                await dbContext.SaveChangesAsync();

                return Ok(eventDetails);
            }
            return NotFound("Event not found!");
        }

        [HttpPut("/slots/{Id:guid}/{inc:bool}")]
        public async Task<ActionResult<Event>> updateSlots(Guid Id, bool inc)
        {
            var eventDetails = await dbContext.Events.FindAsync(Id);
            if (eventDetails != null)
            {
                if (inc == true)
                {
                    eventDetails.CurrentSlots = eventDetails.CurrentSlots + 1;
                    await dbContext.SaveChangesAsync();

                    return Ok(eventDetails.CurrentSlots);
                }
                else
                {
                    if (eventDetails.CurrentSlots > 0)
                    {
                        eventDetails.CurrentSlots = eventDetails.CurrentSlots - 1;
                        await dbContext.SaveChangesAsync();

                        return Ok(eventDetails.CurrentSlots);
                    }
                }
            }
            return NotFound("Event not found!");
        }

        //* DELETE Methods *//
        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult<Event>> deleteEvent(Guid Id)
        {
            var eventDetails = await dbContext.Events.FindAsync(Id);
            if (eventDetails != null)
            {
                dbContext.Events.Remove(eventDetails);
                await dbContext.SaveChangesAsync();

                return Ok(eventDetails);
            }
            return NotFound("Event not found!");
        }
    }
}
