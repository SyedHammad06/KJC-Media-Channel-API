using KJCMediaChannelWebAPI.Data;
using KJCMediaChannelWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KJCMediaChannelWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly APIDbContext dbContext;

        public PostController(APIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //* GET Methods *//
        [HttpGet]
        public async Task<ActionResult<Post>> getPosts()
        {
            return Ok(await dbContext.Posts.ToListAsync());
        }

        [HttpGet("{Id:guid}")]
        public async Task<ActionResult<Post>> getPost(Guid Id)
        {
            var post = await dbContext.Posts.FindAsync(Id);
            if (post != null)
            {
                return Ok(post);
            }
            return NotFound("Post not found");
        }

        //* POST Methods *//
        [HttpPost]
        public async Task<ActionResult<Post>> addPost(PostRequest postRequest)
        {
            var post = new Post()
            {
                Id = Guid.NewGuid(),
                UserId = postRequest.UserId,
                Description = postRequest.Description,
                Department = postRequest.Department,
                Likes = postRequest.Likes,
                Dislikes = postRequest.Dislikes,
                CreatedDate = DateTime.Now,
                ImageLocation = postRequest.ImageLocation,
            };
            await dbContext.Posts.AddAsync(post);
            await dbContext.SaveChangesAsync();

            return Ok(post);
        }

        //* PUT Methods *//
        [HttpPut("likes/{Id:guid}/{inc:bool}")]
        public async Task<ActionResult<Post>> updateLikes(Guid Id, bool inc)
        {
            var post = await dbContext.Posts.FindAsync(Id);
            if (post != null)
            {
                if (inc == true)
                {
                    post.Likes = post.Likes + 1;
                    await dbContext.SaveChangesAsync();

                    return Ok(post.Likes);
                } 
                else
                {
                    if (post.Likes > 0)
                    {
                        post.Likes = post.Likes - 1;
                        await dbContext.SaveChangesAsync();
                    }
                    return Ok(post.Likes);
                }
            }
            return NotFound("Post not found!");
        }

        [HttpPut("dislikes/{Id:guid}/{inc:bool}")]
        public async Task<ActionResult<Post>> updateDislikes(Guid Id, bool inc)
        {
            var post = await dbContext.Posts.FindAsync(Id);
            if (post != null)
            {
                if (inc == true)
                {
                    post.Dislikes = post.Dislikes + 1;
                    await dbContext.SaveChangesAsync();

                    return Ok(post.Dislikes);
                }
                else
                {
                    if (post.Dislikes > 0)
                    {
                        post.Dislikes = post.Dislikes - 1;
                        await dbContext.SaveChangesAsync();
                    }
                    return Ok(post.Dislikes);
                }
            }
            return NotFound("Post not found!");
        }


        //* DELETE Methods *//
        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult<Post>> deletePost(Guid Id)
        {
            var post = await dbContext.Posts.FindAsync(Id);
            if (post != null)
            {
                dbContext.Posts.Remove(post);
                await dbContext.SaveChangesAsync();
                return Ok("Post deleted successfully");
            }
            return NotFound("Post not found!");
        }
    }
}
