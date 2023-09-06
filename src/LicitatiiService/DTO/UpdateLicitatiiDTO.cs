using System.ComponentModel.DataAnnotations;
using LicitatiiService.Models;

namespace LicitatiiService.DTO;

public class UpdateLicitatiiDTO {

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
    
   
}
