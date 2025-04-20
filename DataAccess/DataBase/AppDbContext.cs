using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataBase
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
                
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<ProductWishlist> ProductWishlists { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Address> Addresses { get; set; }
		public DbSet<Category> Categories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<ProductWishlist>()
                .HasKey(pw => new { pw.ProductId, pw.WishlistId });

			// Add Data Seeding for Categories 
			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 1, Name = "Electronics" },
				new Category { Id = 2, Name = "Furniture" }
			);
			
		}

		
	}
}
