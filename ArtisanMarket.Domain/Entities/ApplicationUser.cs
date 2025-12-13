using Microsoft.AspNetCore.Identity;

namespace ArtisanMarket.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    // Navigation properties
    public Shop? Shop { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}



