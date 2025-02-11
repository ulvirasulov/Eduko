using Business.DTOs.Auth;
using Business.Helpers.Exceptions.Common;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

public class AuthController : Controller
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDTO loginDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return View(loginDto);

            var result = await _userService.LoginAsync(loginDto);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("",
                    "Hesabınız müvəqqəti olaraq bloklanıb. Zəhmət olmasa bir az sonra yenidən cəhd edin");
                return View(loginDto);
            }

            ModelState.AddModelError("", "Email və ya şifrə yanlışdır");
            return View(loginDto);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Daxil olarkən xəta baş verdi. Zəhmət olmasa bir az sonra yenidən cəhd edin");
            return View(loginDto);
        }
    }

    [HttpGet]
    public IActionResult Register()
    {
        if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDTO registerDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return View(registerDto);

            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Şifrələr uyğun gəlmir");
                return View(registerDto);
            }

            var result = await _userService.RegisterAsync(registerDto);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Qeydiyyat uğurla tamamlandı. İndi daxil ola bilərsiniz.";
                return RedirectToAction(nameof(Login));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(registerDto);
        }
        catch (ValidationException ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(registerDto);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("",
                "Qeydiyyat zamanı xəta baş verdi. Zəhmət olmasa bir az sonra yenidən cəhd edin");
            return View(registerDto);
        }
    }

    [HttpGet]
    public IActionResult Forgot()
    {
        if (User.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Forgot(string email)
    {
        try
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Email ünvanı daxil edin");
                return View();
            }

            var userExists = await _userService.CheckEmailExistsAsync(email);

            TempData["SuccessMessage"] = "Şifrə yeniləmə linki email ünvanınıza göndərildi";
            return RedirectToAction(nameof(Login));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Xəta baş verdi. Zəhmət olmasa bir az sonra yenidən cəhd edin");
            return View();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        try
        {
            await _userService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}