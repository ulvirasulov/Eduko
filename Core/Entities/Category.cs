using Core.Entities.Common;

namespace Core.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }
    
    public virtual ICollection<Course> Courses { get; set; }
    
    public Category()
    {
        Courses = new HashSet<Course>();
    }
}