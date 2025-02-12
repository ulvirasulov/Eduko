using AutoMapper;
using Business.DTOs.Blog;
using Business.Helpers.Exceptions.Common;
using Business.Helpers.Extension;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

public class BlogController : Controller
{
    private readonly IBlogService _blogService;
    private readonly IMapper _mapper;

    public BlogController(
        IBlogService blogService,
        IMapper mapper)
    {
        _blogService = blogService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var blogs = await _blogService.GetBlogsWithTeacherAsync();
            return View(blogs);
        }
        catch (Exception ex)
        {
            return View(new List<GetBlogDTO>());
        }
    }

    public async Task<IActionResult> BlogDetails(int id)
    {
        try
        {
            var blog = await _blogService.GetBlogDetailsAsync(id);
            return View(blog);
        }
        catch (Exception ex)
        {
            return RedirectToAction(nameof(Index));
        }
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
            return RedirectToAction(nameof(Index));
        }
    }
}