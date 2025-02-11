using Business.DTOs.Common;
using Core.Entities.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Business.DTOs.Course;

public class UpdateCourseDTO : BaseIdDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public IFormFile? Photo { get; set; }
    public int Duration { get; set; }
    public int Lessons { get; set; }
    public string Language { get; set; }
    public string SkillLevel { get; set; }
    public CourseStatus Status { get; set; }
    public int CategoryId { get; set; }
}

public class UpdateCourseDTOValidator : AbstractValidator<UpdateCourseDTO>
{
    public UpdateCourseDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Ad boş ola bilməz!")
            .MaximumLength(200)
            .WithMessage("Ad maksimum 200 simvol ola bilər!");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Təsvir boş ola bilməz!")
            .MaximumLength(4000)
            .WithMessage("Təsvir maksimum 4000 simvol ola bilər!");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Qiymət mənfi ola bilməz!");

        RuleFor(x => x.Duration)
            .GreaterThan(0)
            .WithMessage("Müddət düzgün deyil!");

        RuleFor(x => x.Lessons)
            .GreaterThan(0)
            .WithMessage("Dərs sayı düzgün deyil!");

        RuleFor(x => x.Language)
            .NotEmpty()
            .WithMessage("Dil boş ola bilməz!")
            .MaximumLength(50)
            .WithMessage("Dil maksimum 50 simvol ola bilər!");

        RuleFor(x => x.SkillLevel)
            .NotEmpty()
            .WithMessage("Səviyyə boş ola bilməz!")
            .MaximumLength(50)
            .WithMessage("Səviyyə maksimum 50 simvol ola bilər!");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Status düzgün deyil!");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .WithMessage("Kateqoriya seçilməlidir!");
        
        RuleFor(x => x.Photo)
            .NotEmpty()
            .WithMessage("Şəkil yüklənməlidir!")
            .Must(x => x.ContentType == "image/jpeg" || x.ContentType == "image/png")
            .WithMessage("Şəkil yalnız JPEG və ya PNG formatında olmalıdır!")
            .Must(x => x.Length <= 5 * 1024 * 1024)
            .WithMessage("Şəkil 5MB-dan böyük ola bilməz!");
    }
}
