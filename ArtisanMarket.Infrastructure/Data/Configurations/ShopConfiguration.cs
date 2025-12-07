using ArtisanMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArtisanMarket.Infrastructure.Data.Configurations;

public class ShopConfiguration : IEntityTypeConfiguration<Shop>
{
    public void Configure(EntityTypeBuilder<Shop> builder)
    {
        builder.ToTable("shops");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.UserId)
            .IsRequired();

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Slug)
            .IsRequired()
            .HasMaxLength(110);

        builder.Property(s => s.Description)
            .HasColumnType("text");

        builder.Property(s => s.ContactEmail)
            .HasMaxLength(256);

        builder.Property(s => s.Phone)
            .HasMaxLength(50);

        builder.Property(s => s.DeletedAt)
            .HasColumnType("timestamptz");

        // Unique constraints
        builder.HasIndex(s => s.UserId)
            .IsUnique()
            .HasDatabaseName("idx_shops_user_id");

        builder.HasIndex(s => s.Slug)
            .IsUnique()
            .HasDatabaseName("idx_shops_slug");

        // Foreign key
        builder.HasOne(s => s.User)
            .WithOne(u => u.Shop)
            .HasForeignKey<Shop>(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
