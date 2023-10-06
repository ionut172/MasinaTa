using Models;

namespace BiddingService.DTOs;


public class BidDTO {

    public string ID { get; set; }
    public string licitatieId {get;set;}
    public string vanzator {get;set;}
    public DateTime BidTime {get;set;}
    public int pretRezervare {get;set;}
    public string Status{get;set;}
}