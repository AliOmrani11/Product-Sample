using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain.Entities.Accessory;
using Product.Domain.Entities.Product;
using Product.Domain.Entities.Properties;
using System.Reflection.Emit;

namespace Product.Infrastructure.Data;

public static class DatabaseConfiguration
{

    #region Product

    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("TblProduct");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(s => s.Price)
                .HasMaxLength(250)
                .IsRequired(false);

            builder.Property(s => s.PropertyJson)
                .IsRequired(false);


            builder.Property(s => s.PriceType)
                .IsRequired(false);

            builder.Property(s => s.Inventory)
                .IsRequired(false);

            builder.Property(s => s.Weight)
                .IsRequired(false);

            builder.HasOne(s => s.ProductGroup)
                .WithMany(s => s.Products)
                .HasForeignKey(s => s.ProductGroupId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }


    public class ProductGroupConfiguration : IEntityTypeConfiguration<ProductGroupEntity>
    {
        public void Configure(EntityTypeBuilder<ProductGroupEntity> builder)
        {
            builder.ToTable("TblProductGroup");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(s => s.PhotoUrl)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(s => s.Discount)
                .IsRequired(false);

            builder.Property(s => s.DiscountExpire)
                .IsRequired(false);
        }
    }
    
    
    public class ProductPropertyConfiguration : IEntityTypeConfiguration<ProductPropertyEntity>
    {
        public void Configure(EntityTypeBuilder<ProductPropertyEntity> builder)
        {
            builder.ToTable("TblProductProperty");

            builder.HasKey(s => s.Id);

            builder.HasOne(s => s.PropertyValue)
                .WithMany(s => s.ProductProperties)
                .HasForeignKey(s => s.PropertyValueId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(s => s.Product)
                .WithMany(s => s.ProductProperties)
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }

    #endregion


    #region Property

    public class PropertyTypeConfiguration : IEntityTypeConfiguration<PropertyTypeEntity>
    {
        public void Configure(EntityTypeBuilder<PropertyTypeEntity> builder)
        {
            builder.ToTable("TblPropertyType");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .HasMaxLength(250)
                .IsRequired();

        }
    }


    public class PropertyValueConfiguration : IEntityTypeConfiguration<PropertyValueEntity>
    {
        public void Configure(EntityTypeBuilder<PropertyValueEntity> builder)
        {
            builder.ToTable("TblPropertyValue");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Value)
                .HasMaxLength(500)
                .IsRequired();

            builder.HasOne(s => s.PropertyType)
                .WithMany(s => s.PropertyValues)
                .HasForeignKey(s => s.PropertyTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }


    #endregion


    #region Accessory

    public class AccessoryConfiguration : IEntityTypeConfiguration<AccessoryEntity>
    {
        public void Configure(EntityTypeBuilder<AccessoryEntity> builder)
        {
            builder.ToTable("TblAccessory");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .HasMaxLength(500)
            .IsRequired();

            builder.HasMany(e => e.ProductGroups)
            .WithMany(e => e.Accessories)
            .UsingEntity(
                "TblProductAccessory",
                l => l.HasOne(typeof(ProductGroupEntity)).WithMany().HasForeignKey("ProductGroupId").HasPrincipalKey(nameof(ProductGroupEntity.Id)),
                r => r.HasOne(typeof(AccessoryEntity)).WithMany().HasForeignKey("AccessoryId").HasPrincipalKey(nameof(AccessoryEntity.Id)),
                j => j.HasKey("ProductGroupId", "AccessoryId"));
        }
    }

    #endregion
}
