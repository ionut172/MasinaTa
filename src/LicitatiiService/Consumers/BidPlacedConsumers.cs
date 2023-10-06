using LicitatiiService.Data;
using Contracts;
using MassTransit;

namespace LicitatiiService;

public class BidPlacedConsumer : IConsumer<BidPlaced>
{
    private readonly LicitatiiDBContext _dbContext;

    public BidPlacedConsumer(LicitatiiDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<BidPlaced> context)
    {
        Console.WriteLine("--> Consuming bid placed");

        var licitatie = await _dbContext.Licitatii.FindAsync(Guid.Parse(context.Message.licitatieId));

        if (licitatie.CelMaiMareBid == 0 
            || context.Message.BidStatus.Contains("Accepted") 
            && context.Message.pretRezervare > licitatie.CelMaiMareBid)
        {
            licitatie.CelMaiMareBid = context.Message.pretRezervare;
            await _dbContext.SaveChangesAsync();
        }
    }
}