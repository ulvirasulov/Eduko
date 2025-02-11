using Business.DTOs.Common;
using FluentValidation;

namespace Business.DTOs.Category;

public class UpdateCategoryDTO : BaseIdDTO
{
    public string Name { get; set; }
}

public class UpdateCategoryDTOValidator : AbstractValidator<UpdateCategoryDTO>
{
    public UpdateCategoryDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Ad boş ola bilməz!")
            .MaximumLength(100)
            .WithMessage("Ad maksimum 100 simvol ola bilər!");
    }
}