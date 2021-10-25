using DapperDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DapperDemo.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
    }
}
