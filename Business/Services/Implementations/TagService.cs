using AutoMapper;
using Business.DTOs.Tag;
using Business.Helpers.Exceptions.Common;
using Business.Services.Interfaces;
using Core.Entities;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Business.Services.Implementations;

public class TagService : Service<Tag>, ITagService
{
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<TagService> _logger;

    public TagService(
        ITagRepository tagRepository,
        IMapper mapper,
        ILogger<TagService> logger) : base(tagRepository, logger)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<GetTagDTO>> GetAllTagsAsync()
    {
        try
        {
            var tags = await _tagRepository.Table
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<GetTagDTO>>(tags);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all tags");
            throw;
        }
    }

    public async Task<GetTagDTO> GetTagByIdAsync(int id)
    {
        try
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            if (tag == null)
                throw new NotFoundException($"Tag with ID {id} not found");

            return _mapper.Map<GetTagDTO>(tag);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting tag by ID {Id}", id);
            throw;
        }
    }

    public async Task<Tag> CreateAsync(CreateTagDTO dto)
    {
        try
        {
            if (!await _tagRepository.IsTagNameUniqueAsync(dto.Name))
                throw new ValidationException("Tag name must be unique");

            var tag = _mapper.Map<Tag>(dto);
            await _tagRepository.CreateAsync(tag);
            await _tagRepository.SaveChangesAsync();

            _logger.LogInformation("Tag created successfully with ID {Id}", tag.Id);
            return tag;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating tag");
            throw;
        }
    }

    public async Task UpdateAsync(int id, UpdateTagDTO dto)
    {
        try
        {
            var existingTag = await _tagRepository.GetByIdAsync(id);
            if (existingTag == null)
                throw new NotFoundException($"Tag with ID {id} not found");

            _mapper.Map(dto, existingTag);
            await _tagRepository.UpdateAsync(existingTag);
            await _tagRepository.SaveChangesAsync();

            _logger.LogInformation("Tag updated successfully with ID {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating tag with ID {Id}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            if (tag == null)
                throw new NotFoundException($"Tag with ID {id} not found");

            await _tagRepository.DeleteAsync(tag);
            await _tagRepository.SaveChangesAsync();

            _logger.LogInformation("Tag deleted successfully with ID {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting tag with ID {Id}", id);
            throw;
        }
    }

    public async Task<List<Tag>> GetTagsByBlogIdAsync(int blogId)
    {
        try
        {
            return await _tagRepository.GetTagsByBlogIdAsync(blogId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting tags for blog ID {BlogId}", blogId);
            throw;
        }
    }
}