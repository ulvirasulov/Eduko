using Core.Entities.Common;

namespace Core.Entities;


public class Tag : BaseEntity
{
    public string Name { get; set; }
    
    public virtual ICollection<Blog> Blogs { get; set; }
    
    public Tag()
    {
        Blogs = new HashSet<Blog>();
    }
}