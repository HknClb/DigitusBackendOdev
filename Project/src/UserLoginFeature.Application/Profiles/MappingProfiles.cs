using AutoMapper;
using UserLoginFeature.Application.Features.Auth.Dtos;
using UserLoginFeature.Application.Features.Users.Dtos;

namespace UserLoginFeature.Application.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<SignUpUserDto, CreateUserDto>().ReverseMap();
        }
    }
}
