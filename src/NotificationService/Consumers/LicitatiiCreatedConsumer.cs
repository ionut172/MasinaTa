using Contracts;
using Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Consumers;

public class LicitatiiCreatedConsumer : IConsumer<LicitatiiCreated>
{
    private readonly IHubContext<NotificationsHub> _hubContext;
    public LicitatiiCreatedConsumer(IHubContext<NotificationsHub> hubContext)
    {
        this._hubContext = hubContext;
    }
    public async Task Consume(ConsumeContext<LicitatiiCreated> context)
    {
        Console.WriteLine("Noua licitatie realizata");
        await _hubContext.Clients.All.SendAsync("LicitatieCreated", context.Message);
    }
}