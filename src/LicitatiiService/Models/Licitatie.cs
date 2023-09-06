namespace LicitatiiService.Models;


public class Licitatie 
{
    public Guid Id { get; set; }
    public int PretRezervare { get; set; } = 0;
    public string Vanzator { get; set; }
    public string Castigator {get; set; }
    public int CelMaiMareBid {get;set;}
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdatedAt { get; set;} = DateTime.UtcNow;
    public DateTime LicitatieEnd { get; set; }
    public Status Status {get;set;}
    public ItemDB Item {get;set;}
}