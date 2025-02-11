using AutoMapper;
using Business.DTOs.Category;
using Business.Helpers.Exceptions.Common;
using Business.Services.Interfaces;
using Core.Entities;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Business.Services.Implementations;

public class CategoryService : Service<Category>, ICategoryService
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(
        IRepository<Category> categoryRepository,
        IMapper mapper,
        ILogger<CategoryService> logger)
        : base(categoryRepository, logger)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<GetCategoryDTO>> GetAllWithCoursesAsync()
    {
        try
        {
            var categories = await _categoryRepository.Table
                .Include(c => c.Courses)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<GetCategoryDTO>>(categories);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting categories with courses");
            throw;
        }
    }

    public async Task<GetCategoryDTO?> GetByIdWithCoursesAsync(int id)
    {
        try
        {
            var category = await _categoryRepository.Table
                .Include(c => c.Courses)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                throw new NotFoundException($"Category with ID {id} not found");

            return _mapper.Map<GetCategoryDTO>(category);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting category with courses for ID {Id}", id);
            throw;
        }
    }

    public async Task<Category> CreateAsync(CreateCategoryDTO dto)
    {
        try
        {
            if (dto == null)
                throw new ValidationException("Category data cannot be null");

            if (await _categoryRepository.IsExistAsync(c => c.Name == dto.Name))
                throw new BusinessException("Category with this name already exists");

            var category = _mapper.Map<Category>(dto);
            await _categoryRepository.CreateAsync(category);
            await _categoryRepository.SaveChangesAsync();

            _logger.LogInformation("Category created successfully with ID {Id}", category.Id);
            return category;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating category");
            throw;
        }
    }

    public async Task UpdateAsync(int id, UpdateCategoryDTO dto)
    {
        try
        {
            if (dto == null)
                throw new ValidationException("Category update data cannot be null");

            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new NotFoundException($"Category with ID {id} not found");

            if (await _categoryRepository.IsExistAsync(c => c.Name == dto.Name && c.Id != id))
                throw new BusinessException("Category with this name already exists");

            _mapper.Map(dto, category);
            await _categoryRepository.UpdateAsync(category);
            await _categoryRepository.SaveChangesAsync();

            _logger.LogInformation("Category updated successfully with ID {Id}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating category with ID {Id}", id);
            throw;
        }
    }
}