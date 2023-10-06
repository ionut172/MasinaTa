using LicitatiiService.Data;
using LicitatiiService;
using Contracts;
using MassTransit;

namespace LicitatiiService;

public class AuctionFinishedConsumer : IConsumer<LicitatieFinished>
{
    private readonly LicitatiiDBContext _dbContext;

    public AuctionFinishedConsumer(LicitatiiDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<LicitatieFinished> context)
    {
        Console.WriteLine("--> Consuming licitatie finished");

        var licitatie = await _dbContext.Licitatii.FindAsync(Guid.Parse(context.Message.LicitatieId));

        if (context.Message.ItemSold)
        {
            licitatie.Castigator = context.Message.Castigator;
            licitatie.PretRezervare = context.Message.pretRezervare;
        }

        licitatie.Status = licitatie.PretRezervare > licitatie.PretRezervare
            ? Models.Status.Incheiat : Models.Status.Revervat;

        await _dbContext.SaveChangesAsync();
    }
}