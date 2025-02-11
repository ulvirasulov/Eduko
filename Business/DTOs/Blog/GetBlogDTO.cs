using Business.DTOs.Common;
using Business.DTOs.Tag;

namespace Business.DTOs.Blog;

public class GetBlogDTO : BaseIdDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImgUrl { get; set; }
    public DateTime Date { get; set; }
    public string? TeacherOpinion { get; set; }
    public List<GetTagDTO> Tags { get; set; }
}