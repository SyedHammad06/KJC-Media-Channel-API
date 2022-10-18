using KJCMediaChannelWebAPI.Data;
using KJCMediaChannelWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KJCMediaChannelWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly APIDbContext dbContext;

        public DepartmentController(APIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //* GET Methods *//
        [HttpGet]
        public async Task<ActionResult<Department>> getDepartments()
        {
            return Ok(await dbContext.Departments.ToListAsync());
        }

        //* POST Methods *//
        [HttpPost]
        public async Task<ActionResult<Department>> addDepartment(DepartmentRequest departmentRequest)
        {
            var department = new Department()
            {
                Id = Guid.NewGuid(),
                Name = departmentRequest.Name,
                Acronym = departmentRequest.Acronym,
            };

            await dbContext.Departments.AddAsync(department);
            await dbContext.SaveChangesAsync();

            return Ok(department);
        }

        //* DELETE Methods *//
        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult<Department>> deleteDepartment(Guid Id)
        {
            var department = await dbContext.Departments.FindAsync(Id);
            if (department != null)
            {
                dbContext.Departments.Remove(department);
                await dbContext.SaveChangesAsync();

                return Ok(department);
            }
            return NotFound("Department not found!");
        }
    }
}
