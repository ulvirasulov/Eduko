using FluentValidation;

namespace Business.DTOs.Auth;

public class LoginDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}

public class LoginDtoValidator : AbstractValidator<LoginDTO>
{
    public LoginDtoValidator()
    {
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
    }
}