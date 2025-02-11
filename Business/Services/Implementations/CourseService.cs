using AutoMapper;
using Business.DTOs.Blog;
using Business.DTOs.Course;
using Business.Helpers.Exceptions.Common;
using Business.Helpers.Extension;
using Business.Services.Interfaces;
using Core.Entities;
using Core.Entities.Enums;
using DAL.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace Business.Services.Implementations;

public class CourseService : Service<Course>, ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CourseService> _logger;

    public CourseService(
        ICourseRepository courseRepository,
        IMapper mapper,
        ILogger<CourseService> logger)
        : base(courseRepository, logger)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<GetCourseDTO>> GetCoursesWithTeacherAsync()
    {
        try
        {
            var courses = await _courseRepository.GetCoursesWithTeacherAsync();
            return _mapper.Map<List<GetCourseDTO>>(courses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting courses with teacher");
            throw;
        }
    }

    public async Task<List<GetCourseDTO>> GetCoursesWithCategoryAsync()
    {
        try
        {
            var courses = await _courseRepository.GetCoursesWithCategoryAsync();
            return _mapper.Map<List<GetCourseDTO>>(courses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting courses with category");
            throw;
        }
    }

    public async Task<GetCourseDTO?> GetCourseDetailsAsync(int id)
    {
        try
        {
            var course = await _courseRepository.GetCourseDetailsAsync(id);
            if (course == null)
                throw new NotFoundException($"Course with ID {id} not found");

            return _mapper.Map<GetCourseDTO>(course);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting course details for ID {Id}", id);
            throw;
        }
    }

    public async Task<List<GetCourseDTO>> GetCoursesByTeacherAsync(int teacherId)
    {
        try
        {
            var courses = await _courseRepository.GetCoursesByTeacherAsync(teacherId);
            return _mapper.Map<List<GetCourseDTO>>(courses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting courses for teacher ID {TeacherId}", teacherId);
            throw;
        }
    }

    public async Task<Course> CreateAsync(CreateCourseDTO dto)
    {
        try
        {
            if (dto == null)
                throw new ValidationException("Course data cannot be null");

            if (await _courseRepository.IsExistAsync(c => c.Name == dto.Name))
                throw new BusinessException("Course with this name already exists");

            var course = _mapper.Map<Course>(dto);
            await _courseRepository.CreateAsync(course);
            await _courseRepository.SaveChangesAsync();

            _logger.LogInformation("Course created successfully with ID {Id}", course.Id);
            return course;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating course");
            throw;
        }
    }

    public async Task UpdateAsync(int id, UpdateCourseDTO dto)
    {
        try
        {
            if (dto == null)
                throw new ValidationException("Course update data cannot be null");

            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
                throw new NotFoundException($"Course with ID {id} not found");

            if (await _courseRepository.IsExistAsync(c => c.Name == dto.Name && c.Id != id))
                throw new BusinessException("Course with this name already exists");

            _mapper.Map(dto, course);
            await _courseRepository.UpdateAsync(course);
            await _courseRepository.SaveChangesAsync();

            _logger.LogInformation("Course updated successfully with ID {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating course with ID {Id}", id);
            throw;
        }
    }

    public async Task EnrollStudentAsync(int courseId, string userId)
    {
        try
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null)
                throw new NotFoundException($"Course with ID {courseId} not found");

            if (course.Status != CourseStatus.Active)
                throw new BusinessException("Course is not active");

            if (course.EnrolledStudents.Any(s => s.Id == userId))
                throw new BusinessException("Student is already enrolled in this course");

            course.EnrolledStudentsCount++;
            await _courseRepository.UpdateAsync(course);
            await _courseRepository.SaveChangesAsync();

            _logger.LogInformation("Student {UserId} enrolled in course {CourseId}", userId, courseId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while enrolling student {UserId} in course {CourseId}", userId,
                courseId);
            throw;
        }
    }

    public async Task UpdateStatusAsync(int id, CourseStatus status)
    {
        try
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
                throw new NotFoundException($"Course with ID {id} not found");

            course.Status = status;
            await _courseRepository.UpdateAsync(course);
            await _courseRepository.SaveChangesAsync();

            _logger.LogInformation("Course status updated to {Status} for ID {Id}", status, id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating status for course ID {Id}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var blog = await _courseRepository.GetByIdAsync(id);
            if (blog == null) throw new NotFoundException($"ID: {id} olan course tapılmadı");

            await _courseRepository.DeleteAsync(blog);
            await _courseRepository.SaveChangesAsync();

            _logger.LogInformation("Course uğurla silindi. ID: {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Course silinərkən xəta baş verdi. ID: {Id}", id);
            throw;
        }
    }
}