using Business.DTOs.Common;

namespace Business.DTOs.Lesson;

public class GetLessonDTO : BaseIdDTO
{
    public string Title { get; set; }
    public string ContentUrl { get; set; }
    public int Duration { get; set; }
}