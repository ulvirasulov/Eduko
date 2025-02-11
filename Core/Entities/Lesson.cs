using Core.Entities.Common;

namespace Core.Entities;

public class Lesson : BaseEntity
{
    public string Title { get; set; }
    public string ContentUrl { get; set; }

    public int CourseId { get; set; }
    public virtual Course Course { get; set; }

    public int Duration { get; set; }
}