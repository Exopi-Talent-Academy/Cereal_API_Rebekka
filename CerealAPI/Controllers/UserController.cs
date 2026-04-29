using Microsoft.AspNetCore.Mvc;

namespace Cereal_API.Controllers;

public class UserController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
