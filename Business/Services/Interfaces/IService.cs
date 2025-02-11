using System.Linq.Expressions;
using Core.Entities.Common;

namespace Business.Services.Interfaces;

public interface IService<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression);
    IQueryable<T> GetAll();
    IQueryable<T> FindAll(Expression<Func<T, bool>> expression);
    Task<T> CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task SoftDeleteAsync(T entity);
    Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);
}