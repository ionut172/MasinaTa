using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace Consumers;


public class LicitatiiDeleteConsumer : IConsumer<LicitatiiDeleted>
{
    public async Task Consume(ConsumeContext<LicitatiiDeleted> context)
    {
      var result = await DB.DeleteAsync<Item>(context.Message.Id);

      if(!result.IsAcknowledged) throw new MessageException(typeof(LicitatiiDeleted), "Nu s-a putut sterge licitatia");
    }
}