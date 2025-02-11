using Core.Entities;

namespace DAL.Repository.Interfaces;

public interface ITagRepository : IRepository<Tag>
{
    Task<List<Tag>> GetTagsByBlogIdAsync(int blogId);
    Task<bool> IsTagNameUniqueAsync(string name);
}