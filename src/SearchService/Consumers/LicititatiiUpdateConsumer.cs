using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace Consumers;


public class LicitatiiUpdateConsumer : IConsumer<LicitatiiUpdated>
{
    private IMapper _mapper;

    public LicitatiiUpdateConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<LicitatiiUpdated> context)
    {
        Console.WriteLine("-> Consuming licitatie update" + context.Message.Id);

        var item = _mapper.Map<Item>(context.Message);
        var result = await DB.Update<Item>().Match(a => a.ID == context.Message.Id).ModifyOnly(x => new
        {
            x.Culoare,
            x.ModelMasina,
            x.ImagineUrl,
            x.An,
            x.Kilometraj,

        }, item).ExecuteAsync();
        if (result == null)
        {
            throw new MessageException(typeof(LicitatiiUpdated), "Problema mongo db update");
        }
    }
}