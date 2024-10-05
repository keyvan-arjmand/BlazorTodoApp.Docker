using AutoMapper;
using ToDo.Application.Dtos;
using ToDo.Domain.Entity;

namespace ToDo.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>()
            .ReverseMap();
    }
}