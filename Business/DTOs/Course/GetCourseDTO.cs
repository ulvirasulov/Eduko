using Business.DTOs.Category;
using Business.DTOs.Comment;
using Business.DTOs.Common;
using Business.DTOs.Lesson;
using Core.Entities.Enums;

namespace Business.DTOs.Course;

public class GetCourseDTO : BaseIdDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string? ImgUrl { get; set; }
    public int Duration { get; set; }
    public int Lessons { get; set; }
    public string Language { get; set; }
    public string SkillLevel { get; set; }
    public CourseStatus Status { get; set; }
    public double AverageRating { get; set; }
    public int EnrolledStudentsCount { get; set; }
    public GetCategoryDTO Category { get; set; }
    public List<GetLessonDTO> LessonsCollection { get; set; }
    public List<GetCommentDTO> Comments { get; set; }
}