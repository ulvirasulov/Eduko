using Core.Entities;
using DAL.Context;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Implementations;

public class TagRepository : Repository<Tag>, ITagRepository
{
    private readonly AppDbContext _context;

    public TagRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Tag>> GetTagsByBlogIdAsync(int blogId)
    {
        return await _context.Set<Tag>()
            .Where(t => t.Blogs.Any(b => b.Id == blogId))
            .ToListAsync();
    }

    public async Task<bool> IsTagNameUniqueAsync(string name)
    {
        return !await _context.Set<Tag>()
            .AnyAsync(t => t.Name.ToLower() == name.ToLower());
    }
}