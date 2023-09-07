using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using AutoMapper;
using LicitatiiService.Data;
using LicitatiiService.DTO;
using LicitatiiService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LicitatiiService.Controllers;

[ApiController]
[Route("api/licitatii")]
public class LicitatiiController : ControllerBase {
    private readonly LicitatiiDBContext _context;
    private readonly IMapper _mapper;

    public LicitatiiController(LicitatiiDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task <ActionResult<List<LicitatiiDTO>>> ToateLicitatiile(){
        var licitatii = await _context.Licitatii.IgnoreAutoIncludes().Include(x=>x.Item).OrderBy(x => x.Item.Id).ToListAsync();
        return _mapper.Map<List<LicitatiiDTO>>(licitatii);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<LicitatiiDTO>> LicitatieDupaID( Guid id) {
        var licitatii =  await _context.Licitatii.Include(x=>x.Item).FirstOrDefaultAsync(x=>x.Id == id);
        if( licitatii == null ) {
            return NotFound();
        }
        return _mapper.Map<LicitatiiDTO>(licitatii);
    }
    [HttpPost]
     [Route("create")]
    public async Task<ActionResult<LicitatiiDTO>> CreateLicitatie(CreateLicitatiiDTO licitatieDto){
        var licitatie = _mapper.Map<Licitatie>(licitatieDto);
        licitatie.Vanzator ="Ionut";
        _context.Licitatii.Add(licitatie);
        var result = await _context.SaveChangesAsync() > 0 ;
        if (!result){
            return BadRequest("Nu a fost adaugat nimic!");
        }
        return CreatedAtAction(nameof(LicitatieDupaID), new {licitatie.Id}, _mapper.Map<LicitatiiDTO>(licitatie));
     
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateLicitatie(UpdateLicitatiiDTO licitatieDTO, Guid id) {
    var licitatie = await _context.Licitatii.Include(x => x.Item).FirstOrDefaultAsync(x => x.Id == id);
    
    if (licitatie != null) {
       
        licitatie.Item.Make = licitatieDTO.Make ?? licitatie.Item.Make;
        licitatie.Item.ModelMasina = licitatieDTO.ModelMasina ?? licitatie.Item.ModelMasina;
        licitatie.Item.Culoare = licitatieDTO.Culoare ?? licitatie.Item.Culoare;
        licitatie.Item.Kilometraj = licitatieDTO.Kilometraj ?? licitatie.Item.Kilometraj;
        licitatie.Item.An = licitatieDTO.An ?? licitatie.Item.An;
    
        var result = await _context.SaveChangesAsync() > 0;
        
        if (!result) {
            return BadRequest("Nu s-a facut modificarea");
        }
        
        return Ok();
    } else {
        return NotFound();
    }
}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLicitatie(Guid id){
        var licitatie = await _context.Licitatii.FirstOrDefaultAsync(x=>x.Id == id);
        if (licitatie == null) {
            return BadRequest("Nu s-a gasit licitatia");
        }
        _context.Licitatii.Remove(licitatie);
        var result = _context.SaveChangesAsync();
        if (result != null){
            return Ok();
        }
        else{
            return BadRequest("Nu s-a sters licitatia.");
        }
    }
}