using System.Linq.Expressions;
using AutoMapper;
using Business.Helpers.Exceptions.Common;
using Business.Services.Interfaces;
using Core.Entities.Common;
using DAL.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace Business.Services.Implementations;

public class Service<T> : IService<T> where T : BaseEntity
{
    private readonly IRepository<T> _repository;
    private readonly ILogger<Service<T>> _logger;

    public Service(IRepository<T> repository, ILogger<Service<T>> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"{typeof(T).Name} with ID {id} not found");

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting {EntityName} with ID {Id}", typeof(T).Name, id);
            throw;
        }
    }

    public virtual async Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression)
    {
        try
        {
            var entity = await _repository.GetSingleAsync(expression);
            if (entity == null)
                throw new NotFoundException($"{typeof(T).Name} not found");

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting single {EntityName}", typeof(T).Name);
            throw;
        }
    }

    public virtual IQueryable<T> GetAll()
    {
        try
        {
            return _repository.GetAll();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all {EntityName}s", typeof(T).Name);
            throw;
        }
    }

    public virtual IQueryable<T> FindAll(Expression<Func<T, bool>> expression)
    {
        try
        {
            return _repository.FindAll(expression);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while finding {EntityName}s", typeof(T).Name);
            throw;
        }
    }

    public virtual async Task<T> CreateAsync(T entity)
    {
        try
        {
            if (entity == null)
                throw new ValidationException($"{typeof(T).Name} cannot be null");

            await _repository.CreateAsync(entity);
            await _repository.SaveChangesAsync();

            _logger.LogInformation("{EntityName} created successfully with ID {Id}", typeof(T).Name, entity.Id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating {EntityName}", typeof(T).Name);
            throw;
        }
    }

    public virtual async Task UpdateAsync(T entity)
    {
        try
        {
            if (entity == null)
                throw new ValidationException($"{typeof(T).Name} cannot be null");

            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();

            _logger.LogInformation("{EntityName} updated successfully with ID {Id}", typeof(T).Name, entity.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating {EntityName} with ID {Id}", typeof(T).Name, entity?.Id);
            throw;
        }
    }

    public virtual async Task DeleteAsync(T entity)
    {
        try
        {
            if (entity == null)
                throw new ValidationException($"{typeof(T).Name} cannot be null");

            await _repository.DeleteAsync(entity);
            await _repository.SaveChangesAsync();

            _logger.LogInformation("{EntityName} deleted successfully with ID {Id}", typeof(T).Name, entity.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting {EntityName} with ID {Id}", typeof(T).Name, entity?.Id);
            throw;
        }
    }

    public virtual async Task SoftDeleteAsync(T entity)
    {
        try
        {
            if (entity == null)
                throw new ValidationException($"{typeof(T).Name} cannot be null");

            await _repository.SoftDeleteAsync(entity);
            await _repository.SaveChangesAsync();

            _logger.LogInformation("{EntityName} soft deleted successfully with ID {Id}", typeof(T).Name, entity.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while soft deleting {EntityName} with ID {Id}", typeof(T).Name,
                entity?.Id);
            throw;
        }
    }

    public virtual async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
    {
        try
        {
            return await _repository.IsExistAsync(expression);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while checking existence of {EntityName}", typeof(T).Name);
            throw;
        }
    }
}