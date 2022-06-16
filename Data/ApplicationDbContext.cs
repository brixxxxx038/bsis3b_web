using bsis3b_web.Models;
using Microsoft.EntityFrameworkCore;

namespace bsis3b_web.Data
{
    public class ApplicationDbContext :  DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):
        base(options)
        {

        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Type> Types { get; set; }
    }
} 