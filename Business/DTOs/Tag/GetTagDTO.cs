using Business.DTOs.Blog;
using Business.DTOs.Common;

namespace Business.DTOs.Tag;

public class GetTagDTO : BaseIdDTO
{
    public string Name { get; set; }
}