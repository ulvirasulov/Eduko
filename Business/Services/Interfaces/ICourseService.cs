using Business.DTOs.Course;
using Core.Entities;
using Core.Entities.Enums;

namespace Business.Services.Interfaces;

public interface ICourseService : IService<Course>
{
    Task<List<GetCourseDTO>> GetCoursesWithTeacherAsync();
    Task<List<GetCourseDTO>> GetCoursesWithCategoryAsync();
    Task<GetCourseDTO?> GetCourseDetailsAsync(int id);
    Task<List<GetCourseDTO>> GetCoursesByTeacherAsync(int teacherId);
    Task<Course> CreateAsync(CreateCourseDTO dto);
    Task UpdateAsync(int id, UpdateCourseDTO dto);
    Task DeleteAsync(int id);
    Task EnrollStudentAsync(int courseId, string userId);
    Task UpdateStatusAsync(int id, CourseStatus status);
}