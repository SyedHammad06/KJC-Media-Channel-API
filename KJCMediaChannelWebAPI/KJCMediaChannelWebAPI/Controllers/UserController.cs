using KJCMediaChannelWebAPI.Data;
using KJCMediaChannelWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;

namespace KJCMediaChannelWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly APIDbContext dbContext;

        public UserController(APIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //*** GET METHODS ***//
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await dbContext.Users.ToListAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<User>> GetUser([FromRoute] Guid id)
        {
            var user = await dbContext.Users.FindAsync(id);
            var updatedUser = new UserRequest()
            {
                RegNo = user.RegNo,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                PhoneNo = user.PhoneNo,
                Department = user.Department
            };
            if (user != null)
            {
                return Ok(updatedUser);
            }
            return NotFound("User not found!");
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<Admin>> GetUserFromEmail(string email)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound("Comment not found");
        }

        //*** POST METHODS ***//
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(UserRequest userRequest)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                RegNo = userRequest.RegNo.ToUpper(),
                Username = userRequest.Username,
                Email = userRequest.Email,
                PhoneNo = userRequest.PhoneNo,
                Password = userRequest.Password,
                Department = userRequest.Department,
            };
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return Ok(user);
        }

        //*** PUT METHODS ***//
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<User>> UpdateUser(Guid id, UserRequest userRequest)
        {
            var user = await dbContext.Users.FindAsync(id);
            if (user != null)
            {
                user.RegNo = userRequest.RegNo.ToUpper();
                user.Username = userRequest.Username;
                user.Email = userRequest.Email;
                user.PhoneNo = userRequest.PhoneNo;
                user.Password = userRequest.Password;
                user.Department = userRequest.Department;

                await dbContext.SaveChangesAsync();

                return Ok(user);
            }
            return NotFound("User not found!");
        }

        [HttpPut("role/{id:guid}")]
        public async Task<ActionResult<User>> UpdateUserRole(Guid id, bool role)
        {
            var user = await dbContext.Users.FindAsync(id);
            if (user != null)
            {
                user.MakePost = role;
                await dbContext.SaveChangesAsync();

                return Ok(user);
            }
            return NotFound("User not found!");
        }

        //*** DELETE METHODS ***//
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<User>> DeleteUser(Guid id)
        {
            var user = await dbContext.Users.FindAsync(id);
            if (user != null)
            {
                dbContext.Users.Remove(user);
                await dbContext.SaveChangesAsync();

                return Ok(user);
            }
            return NotFound("User not found!");
        }
    }
}