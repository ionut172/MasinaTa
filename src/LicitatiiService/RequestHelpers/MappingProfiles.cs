using AutoMapper;
using LicitatiiService.DTO;
using LicitatiiService.Models;

namespace LicitatiiService.RequestHelpers;

public class MappingProfiles : Profile {
    public MappingProfiles()
    {
        CreateMap<Licitatie, LicitatiiDTO>().IncludeMembers(x=>x.Item);
        CreateMap<ItemDB, LicitatiiDTO>();
        CreateMap<CreateLicitatiiDTO, Licitatie>().ForMember(d=>d.Item, o=>o.MapFrom (s=>s));
        CreateMap<CreateLicitatiiDTO, ItemDB>();
    }
}