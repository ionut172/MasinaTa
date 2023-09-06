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
    public async Task<ActionResult<LicitatiiDTO>> CreateLicitatie(LicitatiiDTO licitatieDto){
        var licitatie = _mapper.Map<Licitatie>(licitatieDto);
        licitatie.Vanzator ="test";
        _context.Licitatii.Add(licitatie);
        var result = await _context.SaveChangesAsync() > 0 ;
        if (!result){
            return BadRequest("Nu a fost adaugat nimic!");
        }
        return CreatedAtAction(nameof(LicitatieDupaID), new {licitatie.Id}, _mapper.Map<LicitatiiDTO>(licitatie));
    }
}