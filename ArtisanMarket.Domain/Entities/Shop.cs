namespace ArtisanMarket.Domain.Entities;

public class Shop
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ContactEmail { get; set; }
    public string? Phone { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    // Navigation properties
    public ApplicationUser? User { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
