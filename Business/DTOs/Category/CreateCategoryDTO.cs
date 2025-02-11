using FluentValidation;

namespace Business.DTOs.Category;

public class CreateCategoryDTO
{
    public string Name { get; set; }
}

public class CreateCategoryDTOValidator : AbstractValidator<CreateCategoryDTO>
{
    public CreateCategoryDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Ad boş ola bilməz!")
            .MaximumLength(100)
            .WithMessage("Ad maksimum 100 simvol ola bilər!");
    }
}