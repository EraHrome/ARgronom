using Microsoft.EntityFrameworkCore;
using ARgronom.Models;

namespace ARgronom.Contexts
{
    public class Context : DbContext
    {

        public DbSet<IdentityRole> Roles { get; set;}
        public DbSet<Plants> Plants { get; set; }


        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
