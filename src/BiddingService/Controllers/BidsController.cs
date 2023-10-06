using AutoMapper;
using BiddingService.DTOs;
using BiddingService.RequestHelpers;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using MongoDB.Entities;

namespace BiddingService.Controllers;

[ApiController]
[Route("api/bids")]
public class BidsController : ControllerBase {

    private readonly IMapper _mapper;
    private readonly ILogger<BidsController> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public BidsController(ILogger<BidsController> logger, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        this._mapper = mapper;
        this._publishEndpoint = publishEndpoint;
    }
[Authorize]
[HttpPost]
public async Task <ActionResult<Bid>> PlaseazaBid(string licitatieId, int pretRezervare){
try{
         var licitatie = await DB.Find<Licitatie>().OneAsync(licitatieId);
        if (licitatie == null){
            // TODO: verificare cu licitatii svc daca exist licitatii existente
            return NotFound();
        }
        if(licitatie.vanzator == User.Identity.Name){
            return BadRequest("Nu poti licita pentru propria masina.");
        }
        var bid = new Bid{
            pretRezervare = pretRezervare,
            licitatieId = licitatieId,
            vanzator = User.Identity.Name
            };
        if(licitatie.LicitatieDateEnd < DateTime.UtcNow){
            bid.Status = BidStatus.Finished;
        } else {
        var highBid = await DB.Find<Bid>().Match(a => a.ID == licitatieId).Sort(x=>x.Descending(x=>x.pretRezervare)).ExecuteFirstAsync();
        if (highBid != null && pretRezervare > highBid.pretRezervare || highBid == null ) {
            bid.Status = pretRezervare > licitatie.pretRezervare ? BidStatus.Accepted : BidStatus.AcceptedBelowReserve ;
        }
        if (highBid != null && bid.pretRezervare <= highBid.pretRezervare){
            bid.Status = BidStatus.TooLow;
        }
         await DB.SaveAsync(bid);
        await _publishEndpoint.Publish(_mapper.Map<BidPlaced>(bid));

         return Ok(_mapper.Map<BidDTO>(bid));
        }
         }
         catch (Exception ex){
            throw new ArgumentException(ex.ToString());
         }
       
         return NotFound();
       
    }
    [HttpGet("{licitatieId}")]
    public async Task<ActionResult<List<BidDTO>>>GetBids(string licitatieId){
        var bids = await DB.Find<Bid>().Match(a=>a.licitatieId == licitatieId).Sort(b=>b.Descending(a=>a.BidTime)).ExecuteAsync();
        if(bids == null){
            return NotFound();
        }
        Console.WriteLine(bids);
        return bids.Select(_mapper.Map<BidDTO>).ToList();
    }
}