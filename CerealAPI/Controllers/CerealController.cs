using Cereal_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cereal_Api.Controllers;

[Route("api/cereal")]
public class CerealController : Controller
{
    private readonly ICerealRepository _cerealRepository;

    public CerealController(ICerealRepository cerealRepository)
    {
        _cerealRepository = cerealRepository;
    }

    // GET: CerealController
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }

    // GET: CerealController/Details/5
    [HttpGet]
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: CerealController/Create
    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    // POST: CerealController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: CerealController/Edit/5
    [HttpPost]
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: CerealController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: CerealController/Delete/5
    [HttpDelete]
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: CerealController/Delete/5
    [HttpDelete]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
