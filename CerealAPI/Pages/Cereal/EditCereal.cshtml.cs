using Cereal_API.Controllers;
using Cereal_API.Models;
using Cereal_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cereal_API.Pages.Cereal;

public class EditCerealModel : PageModel
{
    private readonly CerealController _controller;

    public EditCerealModel(ICerealRepository repository)
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

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _controller.PostCereal(Cereal.Id, Cereal);

        return RedirectToPage("./ListCereals");
    }
}
