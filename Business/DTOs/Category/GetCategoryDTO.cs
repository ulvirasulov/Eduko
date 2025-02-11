using Business.DTOs.Common;
using Business.DTOs.Course;

namespace Business.DTOs.Category;

public class GetCategoryDTO : BaseIdDTO
{
    public string Name { get; set; }
    public List<GetCourseDTO> Courses { get; set; }
}