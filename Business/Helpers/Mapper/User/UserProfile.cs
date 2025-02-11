using AutoMapper;
using Business.DTOs.User;
using Core.Entities;

namespace Business.Helpers.Mapper.User;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<GetUserDTO, AppUser>().ReverseMap();
    }
}