using System.Text.Json;
using AutoMapper;
using Domain.Models;
using Infrastructure.Database;
using Session = Infrastructure.Database.Session;

namespace Infrastructure.Profiles;

public class DatabaseProfile : Profile
{
    public DatabaseProfile()
    {
        CreateMap<Session, Domain.Models.Session>()
            .ForMember(
                s => s.Configuration,
                opt => opt.MapFrom(
                    o => o.Configuration != null
                        ? JsonDocument.Parse(o.Configuration, default).RootElement
                        : JsonDocument.Parse("{}", default).RootElement)
            );
        CreateMap<SessionItem, Item>()
            .ForMember(
                u => u.Configuration,
                opt => opt.MapFrom(
                    o => JsonDocument.Parse(o.Configuration, default).RootElement)
            );
    }
}