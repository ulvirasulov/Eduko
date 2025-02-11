using Core.Entities.Common;

namespace Core.Entities;

public class Student : BaseEntity
{
    public string Fullname { get; set; }
    public string Email { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }

    public Student()
    {
        Comments = new HashSet<Comment>();
    }
}