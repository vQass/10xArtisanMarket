using ArtisanMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtisanMarket.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("order_items");

        builder.HasKey(oi => oi.Id);
        builder.Property(oi => oi.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(oi => oi.OrderId)
            .IsRequired();

        builder.Property(oi => oi.ProductId)
            .IsRequired();

        builder.Property(oi => oi.ProductName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(oi => oi.ProductPrice)
            .IsRequired()
            .HasColumnType("money");

        builder.Property(oi => oi.Quantity)
            .IsRequired()
            .HasDefaultValue(1);

        // Indexes
        builder.HasIndex(oi => oi.OrderId)
            .HasDatabaseName("idx_order_items_order_id");

        builder.HasIndex(oi => oi.ProductId)
            .HasDatabaseName("idx_order_items_product_id");

        // Foreign keys
        builder.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
