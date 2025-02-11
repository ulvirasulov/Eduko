using Core.Entities;

namespace DAL.Repository.Interfaces;

public interface IBlogRepository : IRepository<Blog>
{
    Task<List<Blog>> GetBlogsWithTeacherAsync();
    Task<List<Blog>> GetBlogsWithTagsAsync();
    Task<Blog?> GetBlogDetailsAsync(int id);
}