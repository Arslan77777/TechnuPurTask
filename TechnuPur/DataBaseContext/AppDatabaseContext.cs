
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechnuPur.Model;
namespace TechnuPur.DatabaseContext
{
    public class AppDatabaseContext : IdentityDbContext<User>
    {
        public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : base(options) { }

        public DbSet<User> Users {  get; set; }
        public DbSet<Product> products  { get; set; }
      
    }
}
