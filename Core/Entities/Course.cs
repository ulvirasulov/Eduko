using Core.Entities.Common;
using Core.Entities.Enums;

namespace Core.Entities;

public class Course : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string? ImgUrl { get; set; }
    public int Duration { get; set; }
    public int Lessons { get; set; }
    public string Language { get; set; }
    public string SkillLevel { get; set; }
    public CourseStatus Status { get; set; } = CourseStatus.Draft;
    public double AverageRating { get; set; }
    public int EnrolledStudentsCount { get; set; }
    public int TeacherId { get; set; }
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<Lesson> LessonsCollection { get; set; }
    public virtual ICollection<AppUser> EnrolledStudents { get; set; }

    public Course()
    {
        Comments = new HashSet<Comment>();
        LessonsCollection = new HashSet<Lesson>();
        EnrolledStudents = new HashSet<AppUser>();
    }
}