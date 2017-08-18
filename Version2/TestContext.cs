using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Version2
{
    public class TestContext : DbContext
    {
        public TestContext()
        {
        }

        public TestContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Map(modelBuilder.Entity<Book>());
            Map(modelBuilder.Entity<Author>());
        }

        private static void Map(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Author).WithMany(x => x.Books).HasForeignKey(x => x.AuthorId);
        }

        private static void Map(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Books);
        }
    }
}
