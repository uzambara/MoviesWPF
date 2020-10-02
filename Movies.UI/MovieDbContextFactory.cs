using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Movies.DAL.Data;

namespace Movies.UI
{
    public class MovieDbContextFactory : IDesignTimeDbContextFactory<MoviesDbContext>
    {
        public MoviesDbContext CreateDbContext(string[] args)
        {
            string connectionString = "Filename=movies.db;";

            var builder = new DbContextOptionsBuilder<MoviesDbContext>();
            builder.UseSqlite(connectionString);
            return new MoviesDbContext(builder.Options);
        }
    }
}