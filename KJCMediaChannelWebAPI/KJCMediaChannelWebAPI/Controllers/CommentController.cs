using KJCMediaChannelWebAPI.Data;
using KJCMediaChannelWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KJCMediaChannelWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly APIDbContext dbContext;

        public CommentController(APIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //* GET Methods *//
        [HttpGet]
        public async Task<ActionResult<Comment>> GetComments()
        {
            return Ok(await dbContext.Comments.ToListAsync());
        }

        [HttpGet("{postId:guid}")]
        public async Task<ActionResult<Comment>> GetComment(Guid postId)
        {
            var comment = dbContext.Comments.Where(comment => comment.PostId == postId);
            if (comment != null)
            {
                return Ok(comment);
            }
            return NotFound("Comment not found");
        }

        //* POST Methods *//
        [HttpPost]
        public async Task<ActionResult<Comment>> AddComment(CommentRequest commentRequest)
        {
            var comment = new Comment()
            {
                Id = Guid.NewGuid(),
                PostId = commentRequest.PostId,
                Description = commentRequest.Description,
                Username = commentRequest.Username
            };
            if (comment != null)
            {
                await dbContext.Comments.AddAsync(comment);
                await dbContext.SaveChangesAsync();
                
                return Ok(comment);
            }
            return BadRequest("comment not added!");
        }

        //* DELETE Methods *//
        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult<Comment>> RemoveComment(Guid Id)
        {
            var comment = await dbContext.Comments.FindAsync(Id);
            if (comment != null)
            {
                dbContext.Comments.Remove(comment);
                await dbContext.SaveChangesAsync();

                return Ok(comment);
            }
            return NotFound("Comment not found!");
        }
    }
}
