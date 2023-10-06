
using Contracts;
using Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Consumers;

public class BidPlacedConsumer : IConsumer<BidPlaced>
{
    private readonly IHubContext<NotificationsHub> _hubContext;
    public BidPlacedConsumer(IHubContext<NotificationsHub> hubContext)
    {
        this._hubContext = hubContext;
    }
    public async Task Consume(ConsumeContext<BidPlaced> context)
    {
        Console.WriteLine("Nou bid realizat");
        await _hubContext.Clients.All.SendAsync("BidPlaced", context.Message);
    }
}