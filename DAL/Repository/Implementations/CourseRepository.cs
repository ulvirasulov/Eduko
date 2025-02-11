using Core.Entities;
using DAL.Context;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Implementations;

public class CourseRepository : Repository<Course>, ICourseRepository
{
    private readonly AppDbContext _context;

    public CourseRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Course>> GetCoursesWithTeacherAsync()
    {
        return await _context.Courses
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Course>> GetCoursesWithCategoryAsync()
    {
        return await _context.Courses
            .Include(c => c.Category)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Course?> GetCourseDetailsAsync(int id)
    {
        return await _context.Courses
            .Include(c => c.Category)
            .Include(c => c.LessonsCollection)
            .Include(c => c.Comments)
            .ThenInclude(cm => cm.AppUser)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Course>> GetCoursesByTeacherAsync(int teacherId)
    {
        return await _context.Courses
            .Where(c => c.TeacherId == teacherId)
            .Include(c => c.Category)
            .AsNoTracking()
            .ToListAsync();
    }
}