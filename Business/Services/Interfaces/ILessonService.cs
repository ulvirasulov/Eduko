using Business.DTOs.Lesson;
using Core.Entities;

namespace Business.Services.Interfaces;

public interface ILessonService : IService<Lesson>
{
    Task<List<GetLessonDTO>> GetCourseLessonsAsync(int courseId);
    Task<Lesson> CreateAsync(CreateLessonDTO dto);
    Task UpdateAsync(int id, UpdateLessonDTO dto);
}