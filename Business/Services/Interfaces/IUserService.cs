using Business.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace Business.Services.Interfaces;

public interface IUserService
{
    Task<IdentityResult> RegisterAsync(RegisterDTO registerDto);
    Task<SignInResult> LoginAsync(LoginDTO loginDto);
    Task LogoutAsync();
    Task<bool> CheckEmailExistsAsync(string email);
    Task<bool> CheckUsernameExistsAsync(string username);
}