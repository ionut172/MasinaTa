using Contracts;
using MassTransit;

namespace LicitatiiService;

public class LicitatiiCreatedFaultsConsumer : IConsumer<Fault<LicitatiiCreated>>
{
    public async Task Consume(ConsumeContext<Fault<LicitatiiCreated>> context)
    {
        Console.WriteLine("--> Consum eroarea de create");

        var exception = context.Message.Exceptions.First();
        if (exception.ExceptionType == "System.ArgumentException"){
            context.Message.Message.Make = "FooBar";
            await context.Publish(context.Message.Message);
        }
        else {
            Console.WriteLine("Nu e o exceptie - error update dashbord");
        }
    }
}