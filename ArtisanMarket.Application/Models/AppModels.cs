using System.ComponentModel.DataAnnotations;

namespace ArtisanMarket.Application.Models;

/// <summary>
/// Data Transfer Objects (DTOs) - niemutowalne modele do wyświetlania danych w UI
/// </summary>
public record UserDto(
    string Id,
    string Email,
    DateTimeOffset CreatedAt
);

public record ShopDto(
    Guid Id,
    string Name,
    string Slug,
    string? Description
);

public record ProductDto(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    ShopDto Shop,
    bool IsActive
);

public record OrderItemDto(
    string ProductName,
    decimal ProductPrice,
    int Quantity,
    decimal Subtotal
);

public record OrderDto(
    Guid Id,
    string Status,
    List<OrderItemDto> Items,
    decimal Total
);

public record OrderDetailsDto(
    Guid Id,
    string Status,
    List<OrderItemDto> Items,
    decimal Total,
    string ShippingFirstName,
    string ShippingLastName,
    string ShippingStreet,
    string ShippingHouseNumber,
    string ShippingPostalCode,
    string ShippingCity,
    string UserEmail
);

/// <summary>
/// Command Models (InputModels) - modele wejściowe dla formularzy z walidacją
/// </summary>
public class UserRegistrationDto
{
    [Required(ErrorMessage = "Adres email jest wymagany")]
    [EmailAddress(ErrorMessage = "Nieprawidłowy format adresu email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Hasło jest wymagane")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Hasło musi mieć co najmniej 8 znaków")]
    public string Password { get; set; } = string.Empty;
}

public class UserLoginDto
{
    [Required(ErrorMessage = "Adres email jest wymagany")]
    [EmailAddress(ErrorMessage = "Nieprawidłowy format adresu email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Hasło jest wymagane")]
    public string Password { get; set; } = string.Empty;
}

public class CreateShopDto
{
    [Required(ErrorMessage = "Nazwa sklepu jest wymagana")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Nazwa sklepu musi mieć od 2 do 100 znaków")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Opis może mieć maksymalnie 500 znaków")]
    public string? Description { get; set; }

    [EmailAddress(ErrorMessage = "Nieprawidłowy format adresu email")]
    public string? ContactEmail { get; set; }

    [StringLength(20, ErrorMessage = "Numer telefonu może mieć maksymalnie 20 znaków")]
    public string? Phone { get; set; }
}

public class UpdateShopDto
{
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Nazwa sklepu musi mieć od 2 do 100 znaków")]
    public string? Name { get; set; }

    [StringLength(500, ErrorMessage = "Opis może mieć maksymalnie 500 znaków")]
    public string? Description { get; set; }

    [EmailAddress(ErrorMessage = "Nieprawidłowy format adresu email")]
    public string? ContactEmail { get; set; }

    [StringLength(20, ErrorMessage = "Numer telefonu może mieć maksymalnie 20 znaków")]
    public string? Phone { get; set; }
}

public class CreateProductDto
{
    [Required(ErrorMessage = "Nazwa produktu jest wymagana")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Nazwa produktu musi mieć od 2 do 200 znaków")]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Opis może mieć maksymalnie 1000 znaków")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Cena jest wymagana")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Cena musi być większa od zera")]
    public decimal Price { get; set; }
}

public class UpdateProductDto
{
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Nazwa produktu musi mieć od 2 do 200 znaków")]
    public string? Name { get; set; }

    [StringLength(1000, ErrorMessage = "Opis może mieć maksymalnie 1000 znaków")]
    public string? Description { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Cena musi być większa od zera")]
    public decimal? Price { get; set; }
}

public class CreateOrderDto
{
    [Required(ErrorMessage = "ID produktu jest wymagane")]
    public Guid ProductId { get; set; }

    [Required(ErrorMessage = "Ilość jest wymagana")]
    [Range(1, int.MaxValue, ErrorMessage = "Ilość musi być większa od zera")]
    public int Quantity { get; set; } = 1;

    [Required(ErrorMessage = "Imię jest wymagane")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Imię musi mieć od 2 do 50 znaków")]
    public string ShippingFirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nazwisko jest wymagane")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Nazwisko musi mieć od 2 do 50 znaków")]
    public string ShippingLastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ulica jest wymagana")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Ulica musi mieć od 2 do 100 znaków")]
    public string ShippingStreet { get; set; } = string.Empty;

    [Required(ErrorMessage = "Numer domu jest wymagany")]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "Numer domu musi mieć od 1 do 20 znaków")]
    public string ShippingHouseNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Kod pocztowy jest wymagany")]
    [StringLength(10, MinimumLength = 3, ErrorMessage = "Kod pocztowy musi mieć od 3 do 10 znaków")]
    public string ShippingPostalCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Miasto jest wymagane")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Miasto musi mieć od 2 do 50 znaków")]
    public string ShippingCity { get; set; } = string.Empty;
}
