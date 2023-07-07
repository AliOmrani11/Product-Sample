using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities.Accessory;
using Product.Domain.Entities.Product;
using Product.Domain.Entities.Properties;
using System.Reflection;

namespace Product.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
        
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    base.OnConfiguring(optionsBuilder);
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<ProductGroupEntity> ProductGroups { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<ProductPropertyEntity> ProductProperties { get; set; }
    public DbSet<PropertyTypeEntity> PropertyTypes { get; set; }
    public DbSet<PropertyValueEntity> PropertyValues { get; set; }
    public DbSet<AccessoryEntity> Accessories { get; set; }
}
