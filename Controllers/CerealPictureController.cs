using Microsoft.AspNetCore.Mvc;

namespace Cereal_API.Controllers;

public class CerealPictureController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
