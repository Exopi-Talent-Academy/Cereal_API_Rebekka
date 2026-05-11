using Cereal_API.Controllers;
using Cereal_API.Models;
using Cereal_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cereal_API.Pages.Cereal;

public class DeleteCerealModel : PageModel
{
    private readonly CerealController _controller;

    public DeleteCerealModel(ICerealRepository repository)
    {
        _controller = new CerealController(repository);
    }

    [BindProperty]
    public Models.Cereal Cereal { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        // Exact same method as DetailsCereal, need to find a way to make a shared method
        if (id == null)
        {
            return NotFound();
        }

        var cereal = await _controller.GetCereal(id.Value);

        if (cereal is not null)
        {
            Cereal = cereal.Value!;

            return Page();
        }

        return NotFound();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        await _controller.DeleteCereal(id.Value);

        return RedirectToPage("./ListCereals");
    }
}
