using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts;
using LicitatiiService.Data;
using LicitatiiService.DTO;
using LicitatiiService.Models;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LicitatiiService.Controllers;

[ApiController]
[Route("api/licitatii")]
public class LicitatiiController : ControllerBase
{
    private readonly LicitatiiDBContext _context;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;
    // 8 sept, dupa 2 zile, am injectat MassTransit cu IpublishEndpoint pentru a trimite mesaj pe api;
    public LicitatiiController(LicitatiiDBContext context, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }
    [HttpGet]
    public async Task<ActionResult<List<LicitatiiDTO>>> ToateLicitatiile(string date)
    {
        var query = _context.Licitatii.OrderBy(x => x.Item.Make).AsQueryable();
        if (!string.IsNullOrEmpty(date))
        {
            query = query.Where(x => x.LastUpdatedAt.CompareTo(DateTime.Parse(date).ToUniversalTime()) > 0);
        }

        return await query.ProjectTo<LicitatiiDTO>(_mapper.ConfigurationProvider).ToListAsync();
        // var licitatii = await _context.Licitatii.IgnoreAutoIncludes().Include(x=>x.Item).OrderBy(x => x.Item.Id).ToListAsync();
        // return _mapper.Map<List<LicitatiiDTO>>(licitatii);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<LicitatiiDTO>> LicitatieDupaID(Guid id)
    {
        var licitatii = await _context.Licitatii.Include(x => x.Item).FirstOrDefaultAsync(x => x.Id == id);
        if (licitatii == null)
        {
            return NotFound();
        }
        return _mapper.Map<LicitatiiDTO>(licitatii);
    }
    [HttpPost]
    [Authorize]
    [Route("create")]


    // adaug mesaj catre mass transit liniile 68-69-70
    public async Task<ActionResult<LicitatiiDTO>> CreateLicitatie(CreateLicitatiiDTO licitatieDto)
    {
        var licitatie = _mapper.Map<Licitatie>(licitatieDto);
        licitatie.Vanzator = User.Identity.Name;
        _context.Licitatii.Add(licitatie);
         var newLicitatie = _mapper.Map<LicitatiiDTO>(licitatie);
        
        await _publishEndpoint.Publish(_mapper.Map<LicitatiiCreated>(newLicitatie));
        var result = await _context.SaveChangesAsync() > 0;
        if (!result)
        {
            return BadRequest("Nu a fost adaugat nimic!");
        }
       
        return CreatedAtAction(nameof(LicitatieDupaID), new { licitatie.Id }, newLicitatie);

    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateLicitatie(UpdateLicitatiiDTO licitatieDTO, Guid id)
    {
        var licitatie = await _context.Licitatii.Include(x => x.Item).FirstOrDefaultAsync(x => x.Id == id);
        if(licitatie.Vanzator != User.Identity.Name) return Forbid();
        if (licitatie != null)
        {

            licitatie.Item.Make = licitatieDTO.Make ?? licitatie.Item.Make;
            licitatie.Item.ModelMasina = licitatieDTO.ModelMasina ?? licitatie.Item.ModelMasina;
            licitatie.Item.Culoare = licitatieDTO.Culoare ?? licitatie.Item.Culoare;
            licitatie.Item.Kilometraj = licitatieDTO.Kilometraj ?? licitatie.Item.Kilometraj;
            licitatie.Item.An = licitatieDTO.An ?? licitatie.Item.An;
            await _publishEndpoint.Publish(_mapper.Map<LicitatiiUpdated>(licitatie));
            var result = await _context.SaveChangesAsync() > 0;

            if (!result)
            {
                return BadRequest("Nu s-a facut modificarea");
            }

            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLicitatie(Guid id)
    {
        var licitatie = await _context.Licitatii.FirstOrDefaultAsync(x => x.Id == id);
        if (licitatie.Vanzator != User.Identity.Name) return Forbid();
        if (licitatie == null)
        {
            return BadRequest("Nu s-a gasit licitatia");
        }
        _context.Licitatii.Remove(licitatie);
        await _publishEndpoint.Publish<LicitatiiDeleted>(new LicitatiiDeleted{ Id = licitatie.Id.ToString()});
        var result = _context.SaveChangesAsync();
        if (result != null)
        {
            return Ok();
        }
        else
        {
            return BadRequest("Nu s-a sters licitatia.");
        }
    }
}