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
                    "biraz sonra cehd et");
                return View(loginDto);
            }

            ModelState.AddModelError("", "email veya sefre yanlisdir");
            return View(loginDto);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "biraz sonra yeniden cehd et");
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
                ModelState.AddModelError("ConfirmPassword", "sifreler ustuste dusmur");
                return View(registerDto);
            }

            var result = await _userService.RegisterAsync(registerDto);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "qeydiyat tamamlandi";
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
                "biraz sonra cehd et");
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
                ModelState.AddModelError("", "email daxil et");
                return View();
            }

            var userExists = await _userService.CheckEmailExistsAsync(email);

            TempData["SuccessMessage"] = "sifreni yenilemek ucun link gonderildi";
            return RedirectToAction(nameof(Login));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "briaz sonra yeniden cehd et");
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