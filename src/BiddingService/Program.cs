using BiddingService.Consumers;
using BiddingService.RequestHelpers;
using BiddingService.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using MongoDB.Driver;
using MongoDB.Entities;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<LicitatieCreatedConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host => {
            host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
            host.Username(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
        });
        cfg.ConfigureEndpoints(context);
    });
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("bids", false));
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServiceUrl"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters.ValidateAudience = false;
        options.TokenValidationParameters.NameClaimType = "username";
        IdentityModelEventSource.ShowPII = true;


    });
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHostedService<CheckLicitatieFinished>();
builder.Services.AddLogging();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();
/// 1. Creez o noua baza de date pentru db bid.
await DB.InitAsync("BidDb", MongoClientSettings.
FromConnectionString(builder.Configuration.GetConnectionString("BidDbConnection")));
app.Run();
