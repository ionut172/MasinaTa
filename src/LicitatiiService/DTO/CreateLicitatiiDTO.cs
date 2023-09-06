using System.ComponentModel.DataAnnotations;

namespace LicitatiiService.DTO;

public class CreateLicitatiiDTO {

    [Required]
    public string Make { get; set; }
    [Required]
    public string ModelMasina { get; set; }
    [Required]
    public int An { get; set; }
    [Required]
    public string Culoare { get; set; }
    [Required]
    public int Kilometraj { get; set; }
    [Required]
    public string ImagineUrl { get; set; }
    [Required]
    public int PretRezervare {get; set; }   
    [Required]
    public DateTime LicitatieEnd {get; set;}
}
