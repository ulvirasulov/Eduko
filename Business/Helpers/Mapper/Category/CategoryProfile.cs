using AutoMapper;
using Business.DTOs.Category;

namespace Business.Helpers.Mapper.Category;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<GetCategoryDTO, Core.Entities.Category>().ReverseMap();
        CreateMap<CreateCategoryDTO, Core.Entities.Category>();
        CreateMap<UpdateCategoryDTO, Core.Entities.Category>();
    }
}