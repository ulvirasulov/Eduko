using Core.Entities.Enums;
using FluentValidation;

namespace Business.DTOs.Auth;

public class RegisterDTO
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public UserType UserType { get; set; }
}

public class RegisterDtoValidator : AbstractValidator<RegisterDTO>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("UserName boş ola bilməz!")
            .MaximumLength(100)
            .WithMessage("UserName maksimum 100 simvol ola bilər!");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email boş ola bilməz!")
            .EmailAddress()
            .WithMessage("Düzgün email formatı daxil edin!");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Şifrə boş ola bilməz!")
            .MinimumLength(6)
            .WithMessage("Şifrə minimum 6 simvol olmalıdır!");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage("Şifrələr eyni olmalıdır!");

        RuleFor(x => x.UserType)
            .IsInEnum()
            .WithMessage("Düzgün istifadəçi tipi seçin!");
    }
}