using MongoDB.Entities;

namespace SearchService.Models;


public class Item : Entity {

    public int PretRezervare { get; set; } = 0;
    public string Vanzator { get; set; }
    public string Castigator {get; set; }
    public int CelMaiMareBid {get;set;}
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdatedAt { get; set;} = DateTime.UtcNow;
    public DateTime LicitatieEnd { get; set; }
    public required string Make { get; set; }
    public required string ModelMasina { get; set; }
    public required int An { get; set; }
    public string  Culoare { get; set; }
    public int Kilometraj { get; set; }
    public string ImagineUrl { get; set; }
}