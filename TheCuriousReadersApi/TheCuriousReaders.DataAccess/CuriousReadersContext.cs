using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheCuriousReaders.DataAccess.Entities;

namespace TheCuriousReaders.DataAccess
{
    public class CuriousReadersContext : IdentityDbContext<UserEntity>
    {
        public CuriousReadersContext(DbContextOptions<CuriousReadersContext> options)
            : base(options) { }

        public DbSet<AddressEntity> Addresses { get; set; }

        public DbSet<BookEntity> Books { get; set; }

        public DbSet<AuthorEntity> Authors { get; set; }

        public DbSet<GenreEntity> Genres { get; set; }

        public DbSet<UserSubscriptionEntity> UserSubscriptions { get; set; }

        public DbSet<CommentEntity> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BookEntity>(entity =>
            {
                entity.HasOne(x => x.Author)
                    .WithMany(x => x.Books)
                    .HasForeignKey(x => x.AuthorId);
            });

            base.OnModelCreating(builder);

            ChangeDefaultIdentityTableNames(builder);
        }

        private static void ChangeDefaultIdentityTableNames(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
        }
    }
}
