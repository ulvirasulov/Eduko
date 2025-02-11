using Business.DTOs.Course;
using Business.Helpers.Extension;
using Business.Services.Interfaces;
using Core.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

public class CourseController : Controller
{
    private readonly ICourseService _courseService;
    private readonly IWebHostEnvironment _env;

    public CourseController(ICourseService courseService, IWebHostEnvironment env)
    {
        _courseService = courseService;
        _env = env;
    }

    public async Task<IActionResult> Index()
    {
        var courses = await _courseService.GetCoursesWithTeacherAsync();
        return View(courses);
    }

    public async Task<IActionResult> CourseSingle(int id)
    {
        var course = await _courseService.GetCourseDetailsAsync(id);
        return View(course);
    }

    [HttpGet]
    /*[Authorize(Roles = "Admin,Teacher")]*/
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    /*[Authorize(Roles = "Admin,Teacher")]*/
    public async Task<IActionResult> Create(CreateCourseDTO dto)
    {
        dto.Photo?.Upload(_env.WebRootPath, "Uploads/Courses");
        await _courseService.CreateAsync(dto);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    /*[Authorize(Roles = "Admin,Teacher")]*/
    public async Task<IActionResult> Update(int id)
    {
        var course = await _courseService.GetCourseDetailsAsync(id);
        var dto = new UpdateCourseDTO
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description,
            Price = course.Price,
            Duration = course.Duration,
            Lessons = course.Lessons,
            Language = course.Language,
            SkillLevel = course.SkillLevel,
            Status = course.Status,
            CategoryId = course.Category.Id
        };
        return View(dto);
    }

    [HttpPost]
    /*[Authorize(Roles = "Admin,Teacher")]*/
    public async Task<IActionResult> Update(int id, UpdateCourseDTO dto)
    {
        dto.Photo?.Upload(_env.WebRootPath, "Uploads/Courses");
        await _courseService.UpdateAsync(id, dto);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    /*[Authorize(Roles = "Admin,Teacher")]*/
    public async Task<IActionResult> Delete(int id)
    {
        await _courseService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    /*[Authorize(Roles = "Admin,Teacher,Student")]*/
    public async Task<IActionResult> Enroll(int courseId)
    {
        var userId = User.FindFirst("sub")?.Value;
        await _courseService.EnrollStudentAsync(courseId, userId);
        return RedirectToAction(nameof(CourseSingle), new { id = courseId });
    }

    [HttpPost]
    /*[Authorize(Roles = "Admin,Teacher")]*/
    public async Task<IActionResult> UpdateStatus(int id, CourseStatus status)
    {
        await _courseService.UpdateStatusAsync(id, status);
        return RedirectToAction(nameof(Index));
    }
}