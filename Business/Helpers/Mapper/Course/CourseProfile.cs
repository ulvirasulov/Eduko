using AutoMapper;
using Business.DTOs.Course;
using Core.Entities.Enums;

namespace Business.Helpers.Mapper.Course;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<GetCourseDTO, Core.Entities.Course>().ReverseMap();

        CreateMap<CreateCourseDTO, Core.Entities.Course>();

        CreateMap<UpdateCourseDTO, Core.Entities.Course>()
            .ForMember(dest => dest.AverageRating, opt => opt.Ignore())
            .ForMember(dest => dest.EnrolledStudentsCount, opt => opt.Ignore());
    }
}