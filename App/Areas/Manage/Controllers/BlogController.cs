using AutoMapper;
using Business.DTOs.Blog;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Areas.Manage.Controllers;
[Area("Manage")]
public class BlogController : Controller
{
    private readonly IBlogService _blogService;
    private readonly ILogger<App.Controllers.BlogController> _logger;
    private readonly IMapper _mapper;

    public BlogController(
        IBlogService blogService,
        ILogger<App.Controllers.BlogController> logger,
        IMapper mapper)
    {
        _blogService = blogService;
        _logger = logger;
        _mapper = mapper;
    }
    [HttpGet]
    /*[Authorize(Roles = "Admin,Teacher")]*/
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    /*[Authorize(Roles = "Admin,Teacher")]*/
    public async Task<IActionResult> Create(CreateBlogDTO dto)
    {
        try
        {
            await _blogService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bloq yaradılarkən xəta baş verdi");
            ModelState.AddModelError("", ex.Message);
            return View(dto);
        }
    }

    [HttpGet]
    /*[Authorize(Roles = "Admin,Teacher")]*/
    public async Task<IActionResult> Update(int id)
    {
        try
        {
            var blog = await _blogService.GetBlogDetailsAsync(id);
            var updateDto = _mapper.Map<UpdateBlogDTO>(blog);
            return View(updateDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bloq məlumatlarını gətirərkən xəta baş verdi. ID: {Id}", id);
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    /*[Authorize(Roles = "Admin,Teacher")]*/
    public async Task<IActionResult> Update(int id, UpdateBlogDTO dto)
    {
        try
        {
            await _blogService.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bloq yenilənərkən xəta baş verdi. ID: {Id}", id);
            ModelState.AddModelError("", ex.Message);
            return View(dto);
        }
    }

    [HttpPost]
    /*[Authorize(Roles = "Admin,Teacher")]*/
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _blogService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bloq silinərkən xəta baş verdi. ID: {Id}", id);
            return RedirectToAction(nameof(Index));
        }
    }
}