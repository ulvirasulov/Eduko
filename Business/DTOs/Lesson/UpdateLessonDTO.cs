using Business.DTOs.Common;
using FluentValidation;

namespace Business.DTOs.Lesson;

public class UpdateLessonDTO : BaseIdDTO
{
    public string Title { get; set; }
    public string ContentUrl { get; set; }
    public int Duration { get; set; }
}

public class UpdateLessonDTOValidator : AbstractValidator<UpdateLessonDTO>
{
    public UpdateLessonDTOValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Başlıq boş ola bilməz!")
            .MaximumLength(200)
            .WithMessage("Başlıq maksimum 200 simvol ola bilər!");

        RuleFor(x => x.ContentUrl)
            .NotEmpty()
            .WithMessage("Kontent linki boş ola bilməz!")
            .MaximumLength(1000)
            .WithMessage("Kontent linki maksimum 1000 simvol ola bilər!");

        RuleFor(x => x.Duration)
            .GreaterThan(0)
            .WithMessage("Müddət düzgün deyil!");
    }
}