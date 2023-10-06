using Duende.IdentityServer;
using IdentityServer.Data;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityServer;

internal static class HostingExtensions
{
   public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
{
    builder.Services.AddRazorPages();

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

    builder.Services
        .AddIdentityServer(options =>
        {
            options.Events.RaiseErrorEvents = true;
            options.Events.RaiseInformationEvents = true;
            options.Events.RaiseFailureEvents = true;
            options.Events.RaiseSuccessEvents = true;
            
            if (builder.Environment.IsEnvironment("Docker"))
                 {
                     options.IssuerUri="identity-svc";
                     Console.WriteLine("Sunt Bine");
                 }
            else {
                options.IssuerUri="identity-svc";
            }
                 
        })
        .AddInMemoryIdentityResources(Config.IdentityResources)
        .AddInMemoryApiScopes(Config.ApiScopes)
        .AddInMemoryClients(Config.Clients)
        .AddAspNetIdentity<ApplicationUser>()
        .AddProfileService<CustomProfileService>();

    builder.Services.ConfigureApplicationCookie(options => 
    {
        options.Cookie.SameSite = SameSiteMode.Lax;
    });

    // Adăugați un provider de logging, de exemplu, Console
    builder.Services.AddLogging(loggingBuilder =>
    {
        loggingBuilder.AddConsole();
    });

    builder.Services.AddAuthentication();

    return builder.Build();
}

    public static WebApplication ConfigurePipeline(this WebApplication app)
    { 
        app.UseSerilogRequestLogging();
    
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();
        
        app.MapRazorPages()
            .RequireAuthorization();

        return app;
    }
}