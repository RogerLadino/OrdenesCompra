using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(e => e.Id)
            .HasName("PK_PRODUCT");

        builder.ToTable("Product");

        builder.HasIndex(e => e.ProductName, "IndexProductName");

        builder.HasIndex(e => e.SupplierId, "IndexProductSupplierId");

        builder.Property(e => e.Package).HasMaxLength(30);
        builder.Property(e => e.ProductName).HasMaxLength(50);
        builder.Property(e => e.UnitPrice)
            .HasDefaultValue(0m)
            .HasColumnType("decimal(12, 2)");

        builder.HasOne(d => d.Supplier)
            .WithMany(p => p.Products)
            .HasForeignKey(d => d.SupplierId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PRODUCT_REFERENCE_SUPPLIER");
    }
}