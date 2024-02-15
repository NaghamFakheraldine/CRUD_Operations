using CRUD_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}
