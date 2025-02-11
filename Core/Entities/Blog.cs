using Core.Entities.Common;

namespace Core.Entities;

public class Blog : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImgUrl { get; set; }
    public DateTime Date { get; set; }
    public string? TeacherOpinion { get; set; }
    
    public int TeacherId { get; set; }
    
    public virtual ICollection<Tag> Tags { get; set; }
    
    public Blog()
    {
        Tags = new HashSet<Tag>();
        Date = DateTime.UtcNow;
    }
}