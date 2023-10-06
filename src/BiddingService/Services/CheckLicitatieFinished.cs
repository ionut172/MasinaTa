

using Contracts;
using MassTransit;
using Models;
using MongoDB.Entities;

namespace BiddingService.Services;


public class CheckLicitatieFinished : BackgroundService, IHostedService
{
    private readonly ILogger _logger;
    private readonly IServiceProvider _serviceProvider;
    public CheckLicitatieFinished(ILogger<CheckLicitatieFinished> logger, IServiceProvider serviceProvider)
    {
        this._logger = logger;
        this._serviceProvider = serviceProvider;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Verific informatiile pentru licitatiile terminate");
        stoppingToken.Register(() => _logger.LogInformation("Licitatie check stop"));
        while (!stoppingToken.IsCancellationRequested)
        {
            await CheckLicitatii(stoppingToken);
            await Task.Delay(5000);
        }
    }

    private async Task CheckLicitatii(CancellationToken stoppingToken)
    {
        var licitatieTerminata = await DB.Find<Licitatie>().Match(x => x.LicitatieDateEnd <= DateTime.UtcNow)
        .Match(y => !y.Finished)
        .ExecuteAsync();
        if (licitatieTerminata.Count == 0)
        {
            return;
        }
        _logger.LogInformation($"Am gasit licitatie terminata in numar de {licitatieTerminata.Count}");
        using var scope = _serviceProvider.CreateScope();
        var endpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
        foreach (var item in licitatieTerminata)
        {
            item.Finished = true;
            await item.SaveAsync(null, stoppingToken);
            var winningBid = await DB.Find<Bid>().Match(a => a.licitatieId == item.ID).Match(b => b.Status == BidStatus.Accepted).Sort(x => x.Descending(s => s.pretRezervare)).ExecuteFirstAsync();
            await endpoint.Publish(new LicitatieFinished
            {
                ItemSold = winningBid != null,
                LicitatieId = item.ID,
                Vanzator = item.vanzator,
                pretRezervare = winningBid.pretRezervare,
                Castigator = winningBid.vanzator

            }, stoppingToken);
        }
    }

}