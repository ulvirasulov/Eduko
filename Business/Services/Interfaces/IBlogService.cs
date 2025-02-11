using Business.DTOs.Blog;
using Core.Entities;

namespace Business.Services.Interfaces;

public interface IBlogService : IService<Blog>
{
    Task<List<GetBlogDTO>> GetBlogsWithTeacherAsync();
    Task<List<GetBlogDTO>> GetBlogsWithTagsAsync();
    Task<GetBlogDTO?> GetBlogDetailsAsync(int id);
    Task<Blog> CreateAsync(CreateBlogDTO dto);
    Task UpdateAsync(int id, UpdateBlogDTO dto);
    Task DeleteAsync(int id);
}