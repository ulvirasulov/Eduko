using AutoMapper;
using Business.DTOs.Blog;

namespace Business.Helpers.Mapper.Blog;

public class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap<GetBlogDTO, Core.Entities.Blog>().ReverseMap();

        CreateMap<CreateBlogDTO, Core.Entities.Blog>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<UpdateBlogDTO, Core.Entities.Blog>()
            .ForMember(dest => dest.Date, opt => opt.Ignore());
    }
}