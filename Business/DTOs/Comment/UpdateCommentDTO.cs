using Business.DTOs.Common;
using FluentValidation;

namespace Business.DTOs.Comment;

public class UpdateCommentDTO : BaseIdDTO
{
    public string Review { get; set; }
    public double Rating { get; set; }
}

public class UpdateCommentDTOValidator : AbstractValidator<UpdateCommentDTO>
{
    public UpdateCommentDTOValidator()
    {
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