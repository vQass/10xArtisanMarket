# Blazor Server Architecture Plan

## 1. Domain Models and DTOs

### Domain Entities (EF Core)
- **User**: Maps to `users` table - authentication and basic user info
- **Shop**: Maps to `shops` table - seller's store with contact details
- **Product**: Maps to `products` table - items for sale with pricing
- **Order**: Maps to `orders` table - purchase records with shipping info
- **OrderItem**: Maps to `order_items` table - order line items with product snapshots
- **OrderStatus**: Maps to `order_statuses` table - order status definitions

### DTOs for UI Layer
- **ShopDto**: Shop details for public catalog (id, name, slug, description)
- **ProductDto**: Product info for display (id, name, description, price, shop info)
- **OrderDto**: Order summary for buyers (id, status, items, total)
- **OrderDetailsDto**: Full order info for sellers (includes shipping address)
- **CreateShopDto**: Shop creation data (name, description, contact info)
- **CreateProductDto**: Product creation data (name, description, price)
- **CreateOrderDto**: Order placement data (productId, quantity, shipping address)
- **UserRegistrationDto**: Account creation data (email, password)
- **UserLoginDto**: Authentication data (email, password)

## 2. Service Layer

### IUserService
**Responsibility:** User account management and authentication

- `Task<Result<UserDto>> RegisterAsync(UserRegistrationDto request)`
  - Validates email format and uniqueness
  - Creates user account with encrypted password
  - Returns success or validation errors

- `Task<Result<UserDto>> LoginAsync(UserLoginDto request)`
  - Authenticates user credentials
  - Returns user info or authentication error

- `Task<UserDto?> GetCurrentUserAsync()`
  - Gets authenticated user info from AuthenticationStateProvider
  - Returns null if not authenticated

### IShopService
**Responsibility:** Shop management for sellers

- `Task<Result<ShopDto>> CreateShopAsync(CreateShopDto request)`
  - Validates user doesn't already have a shop
  - Generates unique slug from shop name
  - Creates shop linked to current user

- `Task<ShopDto?> GetUserShopAsync()`
  - Gets current user's shop (null if none exists)
  - Verifies ownership

- `Task<Result<ShopDto>> UpdateShopAsync(Guid shopId, UpdateShopDto request)`
  - Validates shop ownership
  - Updates shop details and regenerates slug if name changed

- `Task<Result> DeleteShopAsync(Guid shopId)`
  - Soft deletes shop (sets deleted_at)
  - Only if no active orders exist

### IProductService
**Responsibility:** Product CRUD operations

- `Task<Result<ProductDto>> CreateProductAsync(CreateProductDto request)`
  - Validates shop ownership
  - Ensures price > 0
  - Creates product linked to user's shop

- `Task<List<ProductDto>> GetShopProductsAsync(Guid shopId)`
  - Gets all active products for a shop
  - Includes soft-deleted products for owners

- `Task<ProductDto?> GetProductAsync(Guid productId)`
  - Gets product details
  - Public access for catalog browsing

- `Task<Result<ProductDto>> UpdateProductAsync(Guid productId, UpdateProductDto request)`
  - Validates product ownership via shop
  - Updates product details

- `Task<Result> DeleteProductAsync(Guid productId)`
  - Soft deletes product (sets deleted_at)
  - Prevents deletion if product has orders

### IOrderService
**Responsibility:** Order processing and management

- `Task<Result<OrderDto>> CreateOrderAsync(CreateOrderDto request)`
  - Validates user authentication
  - Checks product availability (active, not deleted)
  - Creates order with shipping address
  - Creates order item with product snapshot

- `Task<List<OrderDto>> GetUserOrdersAsync()`
  - Gets current user's purchase history
  - Includes order status and basic item info

- `Task<List<OrderDetailsDto>> GetShopOrdersAsync(Guid shopId)`
  - Gets all orders for seller's shop
  - Includes full shipping details
  - Validates shop ownership

- `Task<OrderDetailsDto?> GetOrderDetailsAsync(Guid orderId)`
  - Gets full order details
  - Buyers see their orders, sellers see shop orders

### ICatalogService
**Responsibility:** Public catalog browsing

- `Task<List<ShopDto>> GetAllShopsAsync()`
  - Gets all active, non-deleted shops
  - Public access, no authentication required

- `Task<ShopDto?> GetShopBySlugAsync(string slug)`
  - Gets shop details by URL slug
  - Includes basic shop info

- `Task<List<ProductDto>> GetShopProductsAsync(string slug)`
  - Gets active products for a shop by slug
  - Public access for catalog browsing

## 3. UI Structure (Pages & Components)

### Public Pages (No Authentication Required)
- **/** or **/catalog**: Shop catalog listing (`Index.razor`)
  - Displays all shops with names and descriptions
  - Links to individual shop pages
  - Injects: `ICatalogService`

- **/shop/{slug}**: Shop details page (`Shop.razor`)
  - Shows shop info and product listing
  - Product cards with "Order" buttons
  - Injects: `ICatalogService`, `NavigationManager`

### Authentication Pages
- **/auth/login**: Login page (`Login.razor`)
  - Email/password form with validation
  - Redirects to dashboard after login
  - Injects: `IUserService`, `AuthenticationStateProvider`

- **/auth/register**: Registration page (`Register.razor`)
  - Email/password registration form
  - Auto-login after successful registration
  - Injects: `IUserService`

### Seller Pages (Shop Owners)
- **/dashboard**: Seller dashboard (`Dashboard.razor`)
  - Shows shop status, recent orders
  - Quick actions for shop management
  - Injects: `IShopService`, `IOrderService`, `IProductService`

- **/dashboard/shop**: Shop management (`ShopManagement.razor`)
  - Create/edit shop details
  - Injects: `IShopService`

- **/dashboard/products**: Product management (`ProductManagement.razor`)
  - List user's products with CRUD actions
  - Add new product form
  - Injects: `IProductService`

- **/dashboard/orders**: Order management (`OrderManagement.razor`)
  - List all orders for seller's shop
  - Order details with shipping info
  - Injects: `IOrderService`

### Buyer Pages (Authenticated Users)
- **/orders**: Order history (`MyOrders.razor`)
  - List of user's purchases
  - Order details and status
  - Injects: `IOrderService`

- **/order/{productId}**: Order placement (`PlaceOrder.razor`)
  - Product details and shipping form
  - Confirmation after successful order
  - Injects: `IProductService`, `IOrderService`

### Shared Components
- **ProductCard**: Displays product info with order button
- **OrderSummary**: Shows order details (buyer/seller views)
- **ShopHeader**: Consistent shop branding
- **Navigation**: Top navigation with auth-aware menu
- **ValidationSummary**: Form validation error display

## 4. Authentication and Authorization

### AuthenticationStateProvider Integration
- Custom `ArtisanAuthenticationStateProvider` extends ASP.NET Core's provider
- Integrates with Identity system for session management
- Services access current user via `AuthenticationStateProvider.GetAuthenticationStateAsync()`

### Authorization Policies
- **ShopOwnerPolicy**: User owns the shop being accessed
- **OrderOwnerPolicy**: User placed the order or owns the shop
- Applied at service method level using `[Authorize(Policy = "ShopOwner")]` attributes

### Security Implementation
- All service methods verify user ownership before data operations
- Database queries filtered by user ID to prevent data leakage
- Soft deletes maintain referential integrity
- Product snapshots in orders prevent price manipulation

## 5. Validation and Business Logic

### Form Validation (Blazor EditForm)
- Client-side validation using DataAnnotations
- Server-side validation in service methods
- Custom validators for business rules (unique shop names, email formats)

### Business Rules Implementation
- **Shop Creation**: One shop per user, unique slug generation
- **Product Management**: Only shop owners can modify products
- **Order Placement**: Authentication required, active products only
- **Data Integrity**: Soft deletes, snapshot preservation

### Error Handling
- Result pattern for service methods with success/failure states
- User-friendly error messages in UI
- Logging for system errors
- Graceful handling of concurrent modifications

### Performance Considerations
- Efficient EF Core queries with proper includes
- Database indexes utilized (as defined in schema)
- Pagination for large lists (products, orders)
- Minimal data transfer between service and UI layers