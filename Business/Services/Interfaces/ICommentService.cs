using Business.DTOs.Comment;
using Business.DTOs.Course;
using Core.Entities;

namespace Business.Services.Interfaces;

public interface ICommentService : IService<Comment>
{
    Task<List<GetCommentDTO>> GetCourseCommentsAsync(int courseId);
    Task<Comment> CreateAsync(CreateCommentDTO dto, string userId);
    Task UpdateAsync(int id, UpdateCommentDTO dto, string userId);
    Task DeleteAsync(int id, string userId);
}