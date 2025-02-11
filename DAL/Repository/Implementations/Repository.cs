using System.Linq.Expressions;
using Core.Entities.Common;
using DAL.Context;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Implementations;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly AppDbContext _context;
    public DbSet<TEntity> Table => _context.Set<TEntity>();

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await Table.FindAsync(id);
    }

    public async Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await Table.FirstOrDefaultAsync(expression);
    }

    public IQueryable<TEntity> GetAll()
    {
        return Table.AsNoTracking().AsQueryable();
    }

    public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression)
    {
        return Table.Where(expression).AsNoTracking().AsQueryable();
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await Table.AddAsync(entity);
        return entity;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        Table.Update(entity);
    }

    public async Task DeleteAsync(TEntity entity)
    {
        Table.Remove(entity);
    }

    public async Task SoftDeleteAsync(TEntity entity)
    {
        entity.IsDeleted = true;
        await UpdateAsync(entity);
    }

    public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await Table.AnyAsync(expression);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}