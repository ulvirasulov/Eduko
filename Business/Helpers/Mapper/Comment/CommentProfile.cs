using AutoMapper;
using Business.DTOs.Comment;

namespace Business.Helpers.Mapper.Comment;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<GetCommentDTO, Core.Entities.Comment>().ReverseMap();

        CreateMap<CreateCommentDTO, Core.Entities.Comment>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<UpdateCommentDTO, Core.Entities.Comment>()
            .ForMember(dest => dest.Date, opt => opt.Ignore());
    }
}