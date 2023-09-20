// EVENT PENTRU RABBITMQ prin MassTransit
namespace Contracts;

public class LicitatiiUpdated
{

    public string Id { get; set; }

    public string Make { get; set; }

    public string ModelMasina { get; set; }

    public int? An { get; set; }

    public string Culoare { get; set; }

    public int? Kilometraj { get; set; }

}