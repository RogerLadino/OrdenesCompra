using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(e => e.Id)
            .HasName("PK_ORDERITEM");

        builder.ToTable("OrderItem");

        builder.HasIndex(e => e.OrderId, "IndexOrderItemOrderId");

        builder.HasIndex(e => e.ProductId, "IndexOrderItemProductId");

        builder.Property(e => e.Quantity).HasDefaultValue(1);
        builder.Property(e => e.UnitPrice).HasColumnType("decimal(12, 2)");

        builder.HasOne(d => d.Order)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(d => d.OrderId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_ORDERITEM_REFERENCE_ORDER");

        builder.HasOne(d => d.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ORDERITEM_REFERENCE_PRODUCT");
    }
}