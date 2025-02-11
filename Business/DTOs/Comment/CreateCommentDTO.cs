using FluentValidation;

namespace Business.DTOs.Comment;

public class CreateCommentDTO
{
    public int CourseId { get; set; }
    public string Review { get; set; }
    public double Rating { get; set; }
}

public class CreateCommentDTOValidator : AbstractValidator<CreateCommentDTO>
{
    public CreateCommentDTOValidator()
    {
        RuleFor(x => x.CourseId)
            .GreaterThan(0)
            .WithMessage("Kurs seçilməlidir!");

        RuleFor(x => x.Review)
            .NotEmpty()
            .WithMessage("Rəy boş ola bilməz!")
            .MaximumLength(2000)
            .WithMessage("Rəy maksimum 2000 simvol ola bilər!");

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5)
            .WithMessage("Reytinq 1-5 arasında olmalıdır!");
    }
}