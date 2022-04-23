using Microsoft.EntityFrameworkCore;
using ARgronom.Models;

namespace ARgronom.Contexts
{
    public class Context : DbContext
    {

        public DbSet<IdentityRole> Roles { get; set;}
        public DbSet<Marker> Markers { get; set; }
        public DbSet<Plants> Plants { get; set; }
        public DbSet<ViewObject> ViewObjects { get; set; }
        public DbSet<SearchPlantsModel> SearchPlantsModels { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
