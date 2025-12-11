using ArtisanMarket.Application.Services;
using ArtisanMarket.BlazorApp.Components;
using ArtisanMarket.BlazorApp.Components.Account;
using ArtisanMarket.Domain.Entities;
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

            builder.Services.AddRazorPages();

            // Register application services
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddScoped<ICatalogService, CatalogService>();

            // Configure Identity
            builder.Services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<ArtisanMarket.Infrastructure.Data.ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            // Configure authentication with Identity
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

            // Register AuthenticationStateProvider for Blazor
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            // Register Identity services for Blazor
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

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

            // Map Identity pages
            app.MapRazorPages();

            // Map Identity endpoints
            app.MapAdditionalIdentityEndpoints();

            app.Run();
        }
    }
}
