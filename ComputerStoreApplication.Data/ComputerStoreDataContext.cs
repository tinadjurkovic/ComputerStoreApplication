using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ComputerStoreApplication.Data.Entities;

namespace ComputerStoreApplication.Data
{
    public class ComputerStoreDataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ComputerStoreDataContext(DbContextOptions<ComputerStoreDataContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("ComputerStoreApplication.Data"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).HasMaxLength(100).IsRequired();
                entity.Property(p => p.Description).HasMaxLength(500);
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();

                entity.HasMany(p => p.Categories)
                      .WithMany(c => c.Products)
                      .UsingEntity(j => j.ToTable("ProductCategories"));
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).HasMaxLength(100).IsRequired();
                entity.Property(c => c.Description).HasMaxLength(500);

                entity.HasData(
                    new Category { Id = 1, Name = "CPU", Description = "Central Processing Unit" },
                    new Category { Id = 2, Name = "Keyboard", Description = "Computer Keyboard" },
                    new Category { Id = 3, Name = "Periphery", Description = "Computer Peripherals" }
                );
            });
        }
    }
}
