using LicitatiiService.Models;

namespace LicitatiiService.DTO;

public class LicitatiiDTO {

    public Guid Id { get; set; }
    public int PretRezervare { get; set; } = 0;
    public string Vanzator { get; set; }
    public string Castigator {get; set; }
    public int CelMaiMareBid {get;set;}
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdatedAt { get; set;} = DateTime.UtcNow;
    public DateTime LicitatieEnd { get; set; }
    public Status Status {get;set;}
     public string Make { get; set; }
    public string ModelMasina { get; set; }
    public int An { get; set; }
    public string Culoare { get; set; }
    public int Kilometraj { get; set; }
    public string ImagineUrl { get; set; }
   
}