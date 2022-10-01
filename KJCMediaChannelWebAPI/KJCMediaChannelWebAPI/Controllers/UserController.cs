using KJCMediaChannelWebAPI.Data;
using KJCMediaChannelWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("{regno}")]
        public async Task<ActionResult<User>> GetUser(string regno)
        {
            var user = await dbContext.Users.FindAsync(regno.ToUpper());
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

        //*** POST METHODS ***//
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(UserRequest userRequest)
        {
            var user = new User()
            {
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
        [HttpPut("{regno}")]
        public async Task<ActionResult<User>> UpdateUser(string regno, UserRequest userRequest)
        {
            var user = await dbContext.Users.FindAsync(regno.ToUpper());
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

        [HttpPut("role/{regno}")]
        public async Task<ActionResult<User>> UpdateUserRole(string regno)
        {
            var user = await dbContext.Users.FindAsync(regno.ToUpper());
            if (user != null)
            {
                user.MakePost = true;
                await dbContext.SaveChangesAsync();

                return Ok(user);
            }
            return NotFound("User not found!");
        }

        //*** DELETE METHODS ***//
        [HttpDelete("{regno}")]
        public async Task<ActionResult<User>> DeleteUser(string regno)
        {
            var user = await dbContext.Users.FindAsync(regno.ToUpper());
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
