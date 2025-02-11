using AutoMapper;
using Business.DTOs.Lesson;
using Business.Helpers.Exceptions.Common;
using Business.Services.Interfaces;
using Core.Entities;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Business.Services.Implementations;

public class LessonService : Service<Lesson>, ILessonService
{
    private readonly IRepository<Lesson> _lessonRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<LessonService> _logger;

    public LessonService(
        IRepository<Lesson> lessonRepository,
        IMapper mapper,
        ILogger<LessonService> logger)
        : base(lessonRepository, logger)
    {
        _lessonRepository = lessonRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<GetLessonDTO>> GetCourseLessonsAsync(int courseId)
    {
        try
        {
            var lessons = await _lessonRepository.Table
                .Where(l => l.CourseId == courseId)
                .OrderBy(l => l.Id)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<GetLessonDTO>>(lessons);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting lessons for course ID {CourseId}", courseId);
            throw;
        }
    }

    public async Task<Lesson> CreateAsync(CreateLessonDTO dto)
    {
        try
        {
            if (dto == null)
                throw new ValidationException("Lesson data cannot be null");

            var lesson = _mapper.Map<Lesson>(dto);
            await _lessonRepository.CreateAsync(lesson);
            await _lessonRepository.SaveChangesAsync();

            _logger.LogInformation("Lesson created successfully with ID {Id}", lesson.Id);
            return lesson;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating lesson");
            throw;
        }
    }

    public async Task UpdateAsync(int id, UpdateLessonDTO dto)
    {
        try
        {
            if (dto == null)
                throw new ValidationException("Lesson update data cannot be null");

            var lesson = await _lessonRepository.GetByIdAsync(id);
            if (lesson == null)
                throw new NotFoundException($"Lesson with ID {id} not found");

            _mapper.Map(dto, lesson);
            await _lessonRepository.UpdateAsync(lesson);
            await _lessonRepository.SaveChangesAsync();

            _logger.LogInformation("Lesson updated successfully with ID {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating lesson with ID {Id}", id);
            throw;
        }
    }
}