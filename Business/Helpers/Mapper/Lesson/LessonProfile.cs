using AutoMapper;
using Business.DTOs.Lesson;

namespace Business.Helpers.Mapper.Lesson;

public class LessonProfile : Profile
{
    public LessonProfile()
    {
        CreateMap<UpdateLessonDTO, Core.Entities.Lesson>();
        CreateMap<CreateLessonDTO, Core.Entities.Lesson>();
        CreateMap<GetLessonDTO, Core.Entities.Lesson>().ReverseMap();
    }
}