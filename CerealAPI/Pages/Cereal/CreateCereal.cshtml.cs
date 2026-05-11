using Cereal_API.Controllers;
using Cereal_API.Models;
using Cereal_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cereal_API.Pages.Cereal;

public class CreateCerealModel : PageModel
{
    private readonly CerealController _controller;

    public CreateCerealModel(ICerealRepository repository)
    {
        _controller = new CerealController(repository);
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Models.Cereal Cereal { get; set; } = default!;

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _controller.PostCereal(Cereal);

        return RedirectToPage("./ListCereals");
    }
}
