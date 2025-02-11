using Core.Entities;

namespace DAL.Repository.Interfaces;

public interface ICourseRepository : IRepository<Course>
{
    Task<List<Course>> GetCoursesWithTeacherAsync();
    Task<List<Course>> GetCoursesWithCategoryAsync();
    Task<Course?> GetCourseDetailsAsync(int id);
    Task<List<Course>> GetCoursesByTeacherAsync(int teacherId);
}