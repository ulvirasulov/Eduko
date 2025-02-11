using Business.DTOs.Common;
using Business.DTOs.User;

namespace Business.DTOs.Comment;

public class GetCommentDTO : BaseIdDTO
{
    public string Review { get; set; }
    public double Rating { get; set; }
    public DateTime Date { get; set; }
    public GetUserDTO AppUser { get; set; }
}