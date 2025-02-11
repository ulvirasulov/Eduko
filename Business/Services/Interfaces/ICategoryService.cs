using Business.DTOs.Category;
using Core.Entities;

namespace Business.Services.Interfaces;

public interface ICategoryService : IService<Category>
{
    Task<List<GetCategoryDTO>> GetAllWithCoursesAsync();
    Task<GetCategoryDTO?> GetByIdWithCoursesAsync(int id);
    Task<Category> CreateAsync(CreateCategoryDTO dto);
    Task UpdateAsync(int id, UpdateCategoryDTO dto);
}