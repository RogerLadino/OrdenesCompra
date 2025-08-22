using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(e => e.Id)
            .HasName("PK_ORDER");

        builder.ToTable("Order");

        builder.HasIndex(e => e.CustomerId, "IndexOrderCustomerId");

        builder.HasIndex(e => e.OrderDate, "IndexOrderOrderDate");

        builder.Property(e => e.OrderDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.OrderNumber).HasMaxLength(10);
        builder.Property(e => e.TotalAmount)
            .HasDefaultValue(0m)
            .HasColumnType("decimal(12, 2)");

        builder.HasOne(d => d.Customer)
            .WithMany(p => p.Orders)
            .HasForeignKey(d => d.CustomerId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ORDER_REFERENCE_CUSTOMER");
    }
}