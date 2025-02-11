using Business.DTOs.Tag;
using Core.Entities;

namespace Business.Services.Interfaces;

public interface ITagService : IService<Tag>
{
    Task<List<GetTagDTO>> GetAllTagsAsync();
    Task<GetTagDTO> GetTagByIdAsync(int id);
    Task<Tag> CreateAsync(CreateTagDTO dto);
    Task UpdateAsync(int id, UpdateTagDTO dto);
    Task DeleteAsync(int id);
    Task<List<Tag>> GetTagsByBlogIdAsync(int blogId);
}