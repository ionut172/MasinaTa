using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace Consumers;


public class BidPlacedConsumer : IConsumer<BidPlaced>
{
    public async Task Consume(ConsumeContext<BidPlaced> context)
    {
      Console.WriteLine("--> Consuming bid placed search");

        var licitatie = await DB.Find<Item>().OneAsync(context.Message.licitatieId);
        if ( licitatie == null ){
            Console.WriteLine("--> NULL search");
        }
        if (
            context.Message.BidStatus.Contains("Accepted") 
            && context.Message.pretRezervare > licitatie.CelMaiMareBid || licitatie.CelMaiMareBid == 0)
        {
            licitatie.CelMaiMareBid = context.Message.pretRezervare;
            await licitatie.SaveAsync();
        }
    }


}