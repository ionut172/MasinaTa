using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService;

public class LicidatiiFinishedConsumer : IConsumer<LicitatieFinished>
{
    public async Task Consume(ConsumeContext<LicitatieFinished> context)
    {
        var auction = await DB.Find<Item>().OneAsync(context.Message.LicitatieId);

        if (context.Message.ItemSold)
        {
            auction.Castigator = context.Message.Castigator;
            auction.PretRezervare = (int)context.Message.pretRezervare;
        }

        auction.Status = "Finished";

        await auction.SaveAsync();
    }
}