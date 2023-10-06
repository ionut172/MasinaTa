using Contracts;
using Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Consumers;

public class LicitatiiFinishedConsumer : IConsumer<LicitatieFinished>
{
    private readonly IHubContext<NotificationsHub> _hubContext;
    public LicitatiiFinishedConsumer(IHubContext<NotificationsHub> hubContext)
    {
        this._hubContext = hubContext;
    }
    public async Task Consume(ConsumeContext<LicitatieFinished> context)
    {
        Console.WriteLine("Noua licitatie finalizata");
        await _hubContext.Clients.All.SendAsync("LicitatieFinished", context.Message);
    }
}