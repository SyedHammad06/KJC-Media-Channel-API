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
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registeration { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<News> News { get; set; }
    }
}
