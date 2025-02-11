using AutoMapper;
using Business.DTOs.Comment;
using Business.Helpers.Exceptions.Common;
using Business.Services.Interfaces;
using Core.Entities;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Business.Services.Implementations;

public class CommentService : Service<Comment>, ICommentService
{
    private readonly IRepository<Comment> _commentRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CommentService> _logger;

    public CommentService(
        IRepository<Comment> commentRepository,
        IMapper mapper,
        ILogger<CommentService> logger)
        : base(commentRepository, logger)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<GetCommentDTO>> GetCourseCommentsAsync(int courseId)
    {
        try
        {
            var comments = await _commentRepository.Table
                .Include(c => c.AppUser)
                .Where(c => c.CourseId == courseId)
                .OrderByDescending(c => c.Date)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<GetCommentDTO>>(comments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting comments for course ID {CourseId}", courseId);
            throw;
        }
    }

    public async Task<Comment> CreateAsync(CreateCommentDTO dto, string userId)
    {
        try
        {
            if (dto == null)
                throw new ValidationException("Comment data cannot be null");

            var comment = _mapper.Map<Comment>(dto);
            comment.AppUserId = userId;

            await _commentRepository.CreateAsync(comment);
            await _commentRepository.SaveChangesAsync();

            _logger.LogInformation("Comment created successfully with ID {Id}", comment.Id);
            return comment;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating comment");
            throw;
        }
    }

    public async Task UpdateAsync(int id, UpdateCommentDTO dto, string userId)
    {
        try
        {
            if (dto == null)
                throw new ValidationException("Comment update data cannot be null");

            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
                throw new NotFoundException($"Comment with ID {id} not found");

            if (comment.AppUserId != userId)
                throw new UnauthorizedException("You are not authorized to update this comment");

            _mapper.Map(dto, comment);
            await _commentRepository.UpdateAsync(comment);
            await _commentRepository.SaveChangesAsync();

            _logger.LogInformation("Comment updated successfully with ID {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating comment with ID {Id}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, string userId)
    {
        try
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
                throw new NotFoundException($"Comment with ID {id} not found");

            if (comment.AppUserId != userId)
                throw new UnauthorizedException("You are not authorized to delete this comment");

            await _commentRepository.DeleteAsync(comment);
            await _commentRepository.SaveChangesAsync();

            _logger.LogInformation("Comment deleted successfully with ID {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting comment with ID {Id}", id);
            throw;
        }
    }
}