namespace ArtisanMarket.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int StatusId { get; set; } = 1;
    public string ShippingFirstName { get; set; } = string.Empty;
    public string ShippingLastName { get; set; } = string.Empty;
    public string ShippingStreet { get; set; } = string.Empty;
    public string ShippingHouseNumber { get; set; } = string.Empty;
    public string ShippingPostalCode { get; set; } = string.Empty;
    public string ShippingCity { get; set; } = string.Empty;
    public DateTimeOffset? DeletedAt { get; set; }

    // Navigation properties
    public ApplicationUser? User { get; set; }
    public OrderStatus? Status { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
