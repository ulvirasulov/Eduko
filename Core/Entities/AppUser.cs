using Core.Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class AppUser : IdentityUser
{
    public string Name { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? Bio { get; set; }
    public UserType UserType { get; set; }
    
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<Course> EnrolledCourses { get; set; }
    
    public AppUser()
    {
        Comments = new HashSet<Comment>();
        EnrolledCourses = new HashSet<Course>();
    }
}