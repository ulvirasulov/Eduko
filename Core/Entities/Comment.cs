using Core.Entities.Common;

namespace Core.Entities;

public class Comment : BaseEntity
{
    public int CourseId { get; set; }
    public virtual Course Course { get; set; }
    
    public string AppUserId { get; set; }
    public virtual AppUser AppUser { get; set; }
    
    public int? StudentId { get; set; }
    public virtual Student Student { get; set; }
    
    public string Review { get; set; }
    public double Rating { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}