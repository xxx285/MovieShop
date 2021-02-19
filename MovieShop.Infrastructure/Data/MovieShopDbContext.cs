using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieShop.Core.Entities;

namespace MovieShop.Infrastructure.Data
{
    public class MovieShopDbContext: DbContext
    {
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // this is using Fluent API
            // Action<EntityTypeBuilder<TEntity>> buildAction
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            // rename the key and col names of the conjunction table
            modelBuilder.Entity<Movie>().HasMany(m => m.Genres).WithMany(g => g.Movies)
                .UsingEntity<Dictionary<string, object>>("MovieGenre",
                    m => m.HasOne<Genre>().WithMany().HasForeignKey("GenreId"),
                    g => g.HasOne<Movie>().WithMany().HasForeignKey("MovieId"));

            modelBuilder.Entity<Role>(ConfigureRole);
            modelBuilder.Entity<User>(ConfigureUser);

            modelBuilder.Entity<User>().HasMany(u => u.Roles).WithMany(r => r.Users)
               .UsingEntity<Dictionary<string, object>>("UserRole",
                   u => u.HasOne<Role>().WithMany().HasForeignKey("UserId"),
                   r => r.HasOne<User>().WithMany().HasForeignKey("RoleId"));

            // inline version of the function
            //builder.HasOne(mc => mc.Movie).WithMany(mc => mc.MovieCasts).HasForeignKey(mc => mc.MovieId);
            //builder.HasOne(mc => mc.Cast).WithMany(mc => mc.MovieCasts).HasForeignKey(mc => mc.CastId);

            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<Favorite>(ConfigureFavorite);
            modelBuilder.Entity<Review>(ConfigureReview);
        }
        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            // we are gonna give rules to our Movie table/entity Fluent API
            builder.ToTable("Movie");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).HasMaxLength(256);
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
        }

        // Fluent API (another approach to control is using annotation)
        private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
        {
            builder.ToTable("Trailer");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
            builder.Property(t => t.Name).HasMaxLength(2084);
        }

        private void ConfigureRole(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).HasMaxLength(20);
        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.FirstName).HasMaxLength(128);
            builder.Property(t => t.LastName).HasMaxLength(128);
            builder.Property(t => t.DateOfBirth).HasPrecision(7);
            builder.Property(t => t.Email).HasMaxLength(256);
            builder.Property(t => t.HashedPassword).HasMaxLength(1024);
            builder.Property(t => t.Salt).HasMaxLength(1024);
            builder.Property(t => t.PhoneNumber).HasMaxLength(16);
            builder.Property(t => t.LockoutEndDate).HasPrecision(7);
            builder.Property(t => t.LastLoginDateTime).HasPrecision(7);
        }

        private void ConfigureCast(EntityTypeBuilder<Cast> builder)
        {
            builder.ToTable("Cast");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).HasMaxLength(128);
            builder.Property(t => t.ProfilePath).HasMaxLength(2084);
        }

        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
        {
            builder.ToTable("MovieCast");
            builder.HasKey(t => new { t.CastId, t.MovieId, t.Character });
            builder.Property(t => t.Character).HasMaxLength(450);
        }

        private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.PurchaseNumber).ValueGeneratedOnAdd();
            builder.HasIndex(p => new { p.UserId, p.MovieId }).IsUnique(); // a user can't buy a movie twice
            builder.Property(p => p.PurchaseDateTime).HasPrecision(7);
        }
        // Many DbSet, DbSet are represented as properties for this class

        private void ConfigureFavorite(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("Favorite");
            builder.HasKey(t => t.Id);
        }

        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");
            builder.HasKey(t => new { t.MovieId, t.UserId});
            builder.Property(t => t.Rating).HasPrecision(3, 2); // or builder.Property(r => r.Rating).HasColumnType("decimal(3, 2)");
        }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Movie> Movies { get; set; }
        
        public DbSet<Trailer> Trailers { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<MovieCast> MovieCasts { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<Favorite> Favorites { get; set; }

        public DbSet<Review> Reviews { get; set; }
    }
}
