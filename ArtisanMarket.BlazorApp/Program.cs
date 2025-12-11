using ArtisanMarket.Application.Services;
using ArtisanMarket.BlazorApp.Components;
using ArtisanMarket.BlazorApp.Components.Account;
using ArtisanMarket.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ArtisanMarket.BlazorApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // Register application services
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddScoped<ICatalogService, CatalogService>();

            // Configure authentication with Identity
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

            // Register AuthenticationStateProvider for Blazor
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            // Add authorization
            builder.Services.AddAuthorizationBuilder();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Add authentication and authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // Map Identity endpoints
            app.MapAdditionalIdentityEndpoints();

            app.Run();
        }
    }
}
