using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Models
{
	public class ECommerceContext:DbContext
	{

        public ECommerceContext(DbContextOptions<ECommerceContext>options):base(options) { 

        }

		public DbSet<Users> Users { get; set; }
		public DbSet<Medicine> Medicines { get; set; }
		public DbSet<Cart> Carts { get; set; }


		public DbSet<order> orders { get; set; }

		public DbSet<OrderItems> OrderItems  { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Users>()
				.HasOne<Cart>(s => s.cart)
				.WithOne(ad => ad.users)
				.HasForeignKey<Cart>(ad => ad.UserId);
		}


	}
}
