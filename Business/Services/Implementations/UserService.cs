using AutoMapper;
using Business.DTOs.Auth;
using Business.Helpers.Exceptions.Common;
using Business.Services.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Business.Services.Implementations;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> _logger;

    public UserService(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IMapper mapper,
        ILogger<UserService> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterDTO registerDto)
    {
        try
        {
            if (await CheckEmailExistsAsync(registerDto.Email))
                throw new ValidationException("Bu email artıq istifadə olunub");

            if (await CheckUsernameExistsAsync(registerDto.UserName))
                throw new ValidationException("Bu istifadəçi adı artıq istifadə olunub");

            var user = new AppUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                UserType = registerDto.UserType
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("İstifadəçi uğurla qeydiyyatdan keçdi: {Email}", registerDto.Email);
            }
            else
            {
                _logger.LogWarning("İstifadəçi qeydiyyatı uğursuz oldu: {Email}", registerDto.Email);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Qeydiyyat zamanı xəta baş verdi: {Email}", registerDto.Email);
            throw;
        }
    }

    public async Task<SignInResult> LoginAsync(LoginDTO loginDto)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            
            if (user == null)
                throw new NotFoundException("İstifadəçi tapılmadı");

            var result = await _signInManager.PasswordSignInAsync(
                user,
                loginDto.Password,
                loginDto.RememberMe,
                lockoutOnFailure: true);

            if (result.Succeeded)
            {
                _logger.LogInformation("İstifadəçi uğurla daxil oldu: {Email}", loginDto.Email);
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("İstifadəçi hesabı bloklanıb: {Email}", loginDto.Email);
                throw new ValidationException("Hesabınız müvəqqəti olaraq bloklanıb. Zəhmət olmasa bir az sonra yenidən cəhd edin");
            }
            else
            {
                _logger.LogWarning("Uğursuz giriş cəhdi: {Email}", loginDto.Email);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Giriş zamanı xəta baş verdi: {Email}", loginDto.Email);
            throw;
        }
    }

    public async Task LogoutAsync()
    {
        try
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("İstifadəçi sistemdən çıxış etdi");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Çıxış zamanı xəta baş verdi");
            throw;
        }
    }

    public async Task<bool> CheckEmailExistsAsync(string email)
    {
        return await _userManager.Users.AnyAsync(x => x.Email == email);
    }

    public async Task<bool> CheckUsernameExistsAsync(string username)
    {
        return await _userManager.Users.AnyAsync(x => x.UserName == username);
    }
}