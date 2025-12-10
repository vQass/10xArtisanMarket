namespace ArtisanMarket.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string EncryptedPassword { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    // Navigation properties
    public Shop? Shop { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}

