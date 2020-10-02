using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.DAL.Data.Domain;

namespace Movies.DAL.Data.Configuration
{
    public class MovieEntityConfiguration: IEntityTypeConfiguration<MovieEntity>
    {
        public void Configure(EntityTypeBuilder<MovieEntity> builder)
        {
            builder.ToTable("Movies");
            builder.HasKey(entity => entity.Id);
        }
    }
}