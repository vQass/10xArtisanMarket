using ArtisanMarket.Application.Services;

namespace ArtisanMarket.BlazorApp
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICatalogService, CatalogService>();
            services.AddScoped<IShopService, ShopService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
