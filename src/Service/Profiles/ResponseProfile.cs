using AutoMapper;
using Domain.Models;
using Service.Models;
using Service.Models.Responses;

namespace Service.Profiles;

public class ResponseProfile : Profile
{
    public ResponseProfile()
    {
        CreateMap<Session, SessionDto>()
            .ForMember(p => p.ExpirationTime,
                opt => opt.MapFrom(p => p.ExpiresAt));
        CreateMap<Session, SessionControlDto>()
            .ForMember(p => p.ExpirationTime,
                opt => opt.MapFrom(p => p.ExpiresAt));
        CreateMap<Item, ItemDto>();
        CreateMap<Item, ItemControlDto>();
    }
}