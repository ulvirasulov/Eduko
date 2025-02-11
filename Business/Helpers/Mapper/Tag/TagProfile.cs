using AutoMapper;
using Business.DTOs.Tag;

namespace Business.Helpers.Mapper.Tag;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<GetTagDTO, Core.Entities.Tag>().ReverseMap();
        CreateMap<UpdateTagDTO, Core.Entities.Tag>();
        CreateMap<CreateTagDTO, Core.Entities.Tag>();
    }
}