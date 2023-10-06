using Consumers;
using Hubs;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMassTransit(x =>
{
   x.AddActivitiesFromNamespaceContaining<LicitatiiCreatedConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host => {
            host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
            host.Username(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
        });
        cfg.ConfigureEndpoints(context);
    });
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("nt", false));
});
builder.Services.AddSignalR();
var app = builder.Build();
app.MapHub<NotificationsHub>("/notifications");
app.MapGet("/", () => "Hello World!");

app.Run();
