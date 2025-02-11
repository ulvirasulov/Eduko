using System.Linq.Expressions;
using Core.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    DbSet<TEntity> Table { get; }
    Task<TEntity?> GetByIdAsync(int id);
    Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> expression);
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task SoftDeleteAsync(TEntity entity);
    Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression);
    Task<int> SaveChangesAsync();
}