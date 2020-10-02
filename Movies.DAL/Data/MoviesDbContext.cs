using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Movies.DAL.Data
{
    public class MoviesDbContext: DbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(MoviesDbContext)));
            base.OnModelCreating(modelBuilder);
        }
    }
}