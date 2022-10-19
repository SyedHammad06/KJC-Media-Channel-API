using KJCMediaChannelWebAPI.Data;
using KJCMediaChannelWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KJCMediaChannelWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly APIDbContext dbContext;

        public NewsController(APIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //* GET Methods *//
        [HttpGet]
        public async Task<ActionResult<News>> getNews()
        {
            return Ok(await dbContext.News.ToListAsync());
        }

        [HttpGet("{Id:guid}")]
        public async Task<ActionResult<News>> getParticularNews(Guid Id)
        {
            var news = await dbContext.News.FindAsync(Id);
            if (news != null)
            {
                return Ok(news);
            }
            return NotFound("News not found!");
        }

        //* POST Methods *//
        [HttpPost]
        public async Task<ActionResult<News>> addNews(NewsRequest newsRequest)
        {
            var news = new News()
            {
                Id = Guid.NewGuid(),
                Title = newsRequest.Title,
                Description = newsRequest.Description,
                ImageLocation = newsRequest.ImageLocation,
            };

            await dbContext.News.AddAsync(news);
            await dbContext.SaveChangesAsync();

            return Ok(news);
        }

        //* PUT Methods *//
        [HttpPut("{Id:guid}")]
        public async Task<ActionResult<News>> updateNews(Guid Id, NewsRequest newsRequest)
        {
            var news = await dbContext.News.FindAsync(Id);
            if (news != null)
            {
                news.Title = newsRequest.Title;
                news.Description = newsRequest.Description;
                news.ImageLocation = newsRequest.ImageLocation;

                await dbContext.SaveChangesAsync();

                return Ok(news);
            }
            return NotFound("News not found!");
        }

        //* DELETE Methods *//
        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult<News>> deleteNews(Guid Id)
        {
            var news = await dbContext.News.FindAsync(Id);
            if (news != null)
            {
                dbContext.News.Remove(news);
                await dbContext.SaveChangesAsync();

                return Ok(news);
            }
            return NotFound("News not found!");
        }
    }
}
