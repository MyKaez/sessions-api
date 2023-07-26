using AutoMapper;
using Domain.Models;
using Service.Models;

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
        CreateMap<Item, UserDto>();
        CreateMap<Item, UserControlDto>();
    }
}