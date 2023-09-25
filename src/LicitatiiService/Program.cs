using LicitatiiService;
using LicitatiiService.Data;
using LicitatiiService.DTO;
using LicitatiiService.RequestHelpers;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Entities;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddControllers();
// Se adauga serviciul de licitatii cu parametri de configurare pentru string connection din appsettings.json pentru postgresql
builder.Services.AddDbContext<LicitatiiDBContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddEndpointsApiExplorer();
// Se adauga automapper-ul Automapper;
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Adaugare MassTransit pentru async
// pentru adaugare clasa de contracte - dotnet new classlib src/Contracts.
// pentru a ezita duplicarea codului, dotnet sln add src/Contracts + dotnet add reference ../../src/Contracts in ambele 
// foldere de servicii;

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host => {
            host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
            host.Username(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
        });
        cfg.ConfigureEndpoints(context);
    });
    x.AddEntityFrameworkOutbox<LicitatiiDBContext>(o => {
        o.QueryDelay = TimeSpan.FromSeconds(10);
        o.UsePostgres();
        o.UseBusOutbox();
        
    });
    x.AddConsumersFromNamespaceContaining<LicitatiiCreatedFaultsConsumer>();
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("licitatii", false));
});
///Serviciu pentru autentificare cu identity server
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>{
    options.Authority = builder.Configuration["IdentityServiceUrl"];
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters.ValidateAudience = false;
    options.TokenValidationParameters.NameClaimType = "username";
});
builder.Services.AddMvc().AddJsonOptions(opt =>
{
    // Face suppress la json values by default ca $id, $values;
    opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();
///Mereu intaintea de auturization;
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
try
{
    DBInitializer.InitDB(app);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    throw new Exception("Eroare", ex);
}

app.Run();

