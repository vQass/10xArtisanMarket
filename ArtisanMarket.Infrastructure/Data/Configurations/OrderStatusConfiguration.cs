using ArtisanMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtisanMarket.Infrastructure.Data.Configurations;

public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.ToTable("order_statuses");

        builder.HasKey(os => os.Id);

        builder.Property(os => os.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(os => os.Description)
            .HasColumnType("text");

        // Unique constraint
        builder.HasIndex(os => os.Name)
            .IsUnique()
            .HasDatabaseName("idx_order_statuses_name");

        // Seed data
        builder.HasData(
            new OrderStatus { Id = 1, Name = "złożone", Description = "Zamówienie zostało złożone przez kupującego" },
            new OrderStatus { Id = 2, Name = "potwierdzone", Description = "Zamówienie zostało potwierdzone przez sprzedawcę" },
            new OrderStatus { Id = 3, Name = "wysłane", Description = "Zamówienie zostało wysłane do kupującego" }
        );
    }
}
