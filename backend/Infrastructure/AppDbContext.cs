using Microsoft.EntityFrameworkCore;
using backend.Models;   // 👈 THIS IS IMPORTANT

namespace backend.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    }
}