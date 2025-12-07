using ArtisanMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtisanMarket.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(o => o.UserId)
            .IsRequired();

        builder.Property(o => o.StatusId)
            .IsRequired()
            .HasDefaultValue(1);

        builder.Property(o => o.ShippingFirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(o => o.ShippingLastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(o => o.ShippingStreet)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(o => o.ShippingHouseNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(o => o.ShippingPostalCode)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(o => o.ShippingCity)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(o => o.DeletedAt)
            .HasColumnType("timestamptz");

        // Indexes
        builder.HasIndex(o => o.UserId)
            .HasDatabaseName("idx_orders_user_id");

        // Foreign keys
        builder.HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Status)
            .WithMany(s => s.Orders)
            .HasForeignKey(o => o.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
