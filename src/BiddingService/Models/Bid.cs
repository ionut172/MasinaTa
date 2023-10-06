using MongoDB.Entities;

namespace Models;


public class Bid : Entity {
    public string licitatieId {get;set;}
    public string vanzator {get;set;}
    public DateTime BidTime {get;set;} = DateTime.UtcNow;
    
    public int pretRezervare {get;set;}
    public BidStatus Status{get;set;}
}