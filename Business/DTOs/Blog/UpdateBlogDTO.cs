using Business.DTOs.Common;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Business.DTOs.Blog;

public class UpdateBlogDTO : BaseIdDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile? Photo { get; set; }
    public string? TeacherOpinion { get; set; }
    public List<int> TagIds { get; set; } = new();
}

public class UpdateBlogDTOValidator : AbstractValidator<UpdateBlogDTO>
{
    public UpdateBlogDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Ad boş ola bilməz!")
            .MaximumLength(200)
            .WithMessage("Ad maksimum 200 simvol ola bilər!");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Təsvir boş ola bilməz!");

        RuleFor(x => x.TeacherOpinion)
            .MaximumLength(2000)
            .WithMessage("Müəllim rəyi maksimum 2000 simvol ola bilər!");
        
        RuleFor(x => x.Photo)
            .NotEmpty()
            .WithMessage("Şəkil yüklənməlidir!")
            .Must(x => x.ContentType == "image/jpeg" || x.ContentType == "image/png")
            .WithMessage("Şəkil yalnız JPEG və ya PNG formatında olmalıdır!")
            .Must(x => x.Length <= 5 * 1024 * 1024)
            .WithMessage("Şəkil 5MB-dan böyük ola bilməz!");
    }
}
