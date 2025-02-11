using FluentValidation;

namespace Business.DTOs.Tag;

public class CreateTagDTO
{
    public string Name { get; set; }
}

public class CreateTagDTOValidator : AbstractValidator<CreateTagDTO>
{
    public CreateTagDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Ad boş ola bilməz!")
            .MaximumLength(50)
            .WithMessage("Ad maksimum 50 simvol ola bilər!");
    }
}