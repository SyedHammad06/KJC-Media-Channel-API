using KJCMediaChannelWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KJCMediaChannelWebAPI.Data
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
