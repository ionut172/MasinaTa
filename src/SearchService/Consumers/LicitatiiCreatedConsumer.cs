using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.RequestHelpers;
namespace SearchService.Consumers;


public class LicitatiiCreatedConsumer : IConsumer<LicitatiiCreated>
{
    private readonly IMapper _mapper;
    public LicitatiiCreatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<LicitatiiCreated> context)
    {
        try
        {
            Console.WriteLine("---> Consuming licitatie created " + context.Message.Id);
            // Mapam in <Item> pe care il luam din (context.Message);
            var item = _mapper.Map<Item>(context.Message);
            await item.SaveAsync();
            Console.WriteLine("---> Success " + context.Message.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.WriteLine("---> Eroare ");
            Console.WriteLine("---> Eroare ");
            Console.WriteLine("---> Eroare ");
            Console.WriteLine("---> Eroare ");

        }


    }
}

