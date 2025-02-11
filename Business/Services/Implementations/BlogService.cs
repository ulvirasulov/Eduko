using AutoMapper;
using Business.DTOs.Blog;
using Business.Helpers.Exceptions.Common;
using Business.Helpers.Extension;
using Business.Services.Interfaces;
using Core.Entities;
using DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Business.Services.Implementations;

public class BlogService : Service<Blog>, IBlogService
{
    private readonly IBlogRepository _blogRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<BlogService> _logger;
    private readonly IWebHostEnvironment _env;

    public BlogService(
        IBlogRepository blogRepository,
        IMapper mapper,
        ILogger<BlogService> logger,
        IWebHostEnvironment env)
        : base(blogRepository, logger)
    {
        _blogRepository = blogRepository;
        _mapper = mapper;
        _logger = logger;
        _env = env;
    }

    public async Task<List<GetBlogDTO>> GetBlogsWithTeacherAsync()
    {
        try
        {
            var blogs = await _blogRepository.GetBlogsWithTeacherAsync();
            return _mapper.Map<List<GetBlogDTO>>(blogs);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting blogs with teacher");
            throw;
        }
    }

    public async Task<List<GetBlogDTO>> GetBlogsWithTagsAsync()
    {
        try
        {
            var blogs = await _blogRepository.GetBlogsWithTagsAsync();
            return _mapper.Map<List<GetBlogDTO>>(blogs);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting blogs with tags");
            throw;
        }
    }

    public async Task<GetBlogDTO?> GetBlogDetailsAsync(int id)
    {
        try
        {
            var blog = await _blogRepository.GetBlogDetailsAsync(id);
            if (blog == null)
                throw new NotFoundException($"Blog with ID {id} not found");

            return _mapper.Map<GetBlogDTO>(blog);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting blog details for ID {Id}", id);
            throw;
        }
    }

    public async Task<Blog> CreateAsync(CreateBlogDTO dto)
    {
        try
        {
            if (dto == null)
                throw new ValidationException("Blog data cannot be null");

            if (await _blogRepository.IsExistAsync(b => b.Name == dto.Name))
                throw new BusinessException("Blog with this name already exists");

            var blog = _mapper.Map<Blog>(dto);

            if (dto.Photo != null)
            {
                blog.ImgUrl = dto.Photo.Upload(_env.WebRootPath, "Uploads/Blogs");
            }

            await _blogRepository.CreateAsync(blog);
            await _blogRepository.SaveChangesAsync();

            _logger.LogInformation("Blog created successfully with ID {Id}", blog.Id);
            return blog;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating blog");
            throw;
        }
    }

    public async Task UpdateAsync(int id, UpdateBlogDTO dto)
    {
        try
        {
            if (dto == null)
                throw new ValidationException("Blog update data cannot be null");

            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null)
                throw new NotFoundException($"Blog with ID {id} not found");

            if (await _blogRepository.IsExistAsync(b => b.Name == dto.Name && b.Id != id))
                throw new BusinessException("Blog with this name already exists");

            if (dto.Photo != null)
            {
                if (!string.IsNullOrEmpty(blog.ImgUrl))
                {
                    FileExtension.DeleteFile(_env.WebRootPath, "Uploads/Blogs", blog.ImgUrl);
                }

                blog.ImgUrl = dto.Photo.Upload(_env.WebRootPath, "Uploads/Blogs");
            }

            _mapper.Map(dto, blog);
            await _blogRepository.UpdateAsync(blog);
            await _blogRepository.SaveChangesAsync();

            _logger.LogInformation("Blog updated successfully with ID {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating blog with ID {Id}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null) throw new NotFoundException($"ID: {id} olan bloq tapılmadı");

            if (!string.IsNullOrEmpty(blog.ImgUrl))
            {
                FileExtension.DeleteFile(_env.WebRootPath, "Uploads/Blogs", blog.ImgUrl);
            }

            await _blogRepository.DeleteAsync(blog);
            await _blogRepository.SaveChangesAsync();

            _logger.LogInformation("Bloq uğurla silindi. ID: {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bloq silinərkən xəta baş verdi. ID: {Id}", id);
            throw;
        }
    }
}