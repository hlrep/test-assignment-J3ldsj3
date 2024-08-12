using AutoMapper;

namespace Web.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Web.Models.User, Infrastructure.Models.User>();
    }
}
