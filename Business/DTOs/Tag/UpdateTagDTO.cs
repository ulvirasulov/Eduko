using Business.DTOs.Common;
using FluentValidation;

namespace Business.DTOs.Tag;

public class UpdateTagDTO : BaseIdDTO
{
    public string Name { get; set; }
}

public class UpdateTagDTOValidator : AbstractValidator<UpdateTagDTO>
{
    public UpdateTagDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Ad boş ola bilməz!")
            .MaximumLength(50)
            .WithMessage("Ad maksimum 50 simvol ola bilər!");
    }
}