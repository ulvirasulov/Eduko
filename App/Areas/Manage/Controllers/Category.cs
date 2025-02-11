using Microsoft.AspNetCore.Mvc;

namespace App.Areas.Manage.Controllers;

public class Category : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}