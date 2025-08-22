using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Persistence.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.HasKey(e => e.Id)
            .HasName("PK_SUPPLIER");

        builder.ToTable("Supplier");

        builder.HasIndex(e => e.Country, "IndexSupplierCountry");

        builder.HasIndex(e => e.CompanyName, "IndexSupplierName");

        builder.Property(e => e.City).HasMaxLength(40);
        builder.Property(e => e.CompanyName).HasMaxLength(40);
        builder.Property(e => e.ContactName).HasMaxLength(50);
        builder.Property(e => e.ContactTitle).HasMaxLength(40);
        builder.Property(e => e.Country).HasMaxLength(40);
        builder.Property(e => e.Fax).HasMaxLength(30);
        builder.Property(e => e.Phone).HasMaxLength(30);
    }
}