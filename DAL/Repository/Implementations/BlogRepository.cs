using Core.Entities;
using DAL.Context;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Implementations;

public class BlogRepository : Repository<Blog>, IBlogRepository
{
    private readonly AppDbContext _context;

    public BlogRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Blog>> GetBlogsWithTeacherAsync()
    {
        return await _context.Blogs
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Blog>> GetBlogsWithTagsAsync()
    {
        return await _context.Blogs
            .Include(b => b.Tags)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Blog?> GetBlogDetailsAsync(int id)
    {
        return await _context.Blogs
            .Include(b => b.Tags)
            .FirstOrDefaultAsync(b => b.Id == id);
    }
}