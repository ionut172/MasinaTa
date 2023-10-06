using Contracts;
using MassTransit;
using Models;
using MongoDB.Entities;

namespace BiddingService.Consumers;


public class LicitatieCreatedConsumer : IConsumer<LicitatiiCreated>
{
    public async Task Consume(ConsumeContext<LicitatiiCreated> context)
    {
       var licitatie = new Licitatie {
        ID = context.Message.Id.ToString(),
        vanzator = context.Message.Vanzator,
        LicitatieDateEnd = context.Message.LicitatieEnd,
        pretRezervare = context.Message.PretRezervare

       };
       await licitatie.SaveAsync();
    }
}

