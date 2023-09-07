using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Entities;
namespace LicitatiiService.Models;

[Table("Items")]
public class ItemDB 
{
    public Guid Id { get; set; }
    public string Make { get; set; }
    public string ModelMasina { get; set; }
    public int An { get; set; }
    public string Culoare { get; set; }
    public int Kilometraj { get; set; }
    public string ImagineUrl { get; set; }

    public Licitatie Licitatie { get; set; }
    public Guid LicitatieId { get; set; }
}