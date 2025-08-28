using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(e => e.Id)
            .HasName("PK_CUSTOMER");

        builder.ToTable("Customer");

        builder.HasIndex(e => new { e.LastName, e.FirstName }, "IndexCustomerName");
        builder.Property(e => e.City).HasMaxLength(40);
        builder.Property(e => e.Country).HasMaxLength(40);
        builder.Property(e => e.FirstName).HasMaxLength(40);
        builder.Property(e => e.LastName).HasMaxLength(40);
        builder.Property(e => e.Phone).HasMaxLength(20);
        builder.Property(e => e.Email).HasMaxLength(50);

        builder.Property(e => e.Age)
            .HasComputedColumnSql("DATEDIFF(YEAR, BirthDate, GETDATE())", stored: false)
            .ValueGeneratedOnAddOrUpdate()
            .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);
    }
}