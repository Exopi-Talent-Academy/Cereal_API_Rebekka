using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cereal_API.Models;

namespace Cereal_API.Pages.Cereal;

public class CreateCerealModel : PageModel
{
    private readonly Cereal_API.Models.CerealDbContext _context;

    public CreateCerealModel(Cereal_API.Models.CerealDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Cereal_API.Models.Cereal Cereal { get; set; } = default!;

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Cereals.Add(Cereal);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
