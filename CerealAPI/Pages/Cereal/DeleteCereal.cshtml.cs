using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cereal_API.Models;

namespace Cereal_API.Pages.Cereal;

public class DeleteCerealModel : PageModel
{
    private readonly Cereal_API.Models.CerealDbContext _context;

    public DeleteCerealModel(Cereal_API.Models.CerealDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Cereal_API.Models.Cereal Cereal { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cereal = await _context.Cereals.FirstOrDefaultAsync(m => m.Id == id);

        if (cereal is not null)
        {
            Cereal = cereal;

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

        var cereal = await _context.Cereals.FindAsync(id);
        if (cereal != null)
        {
            Cereal = cereal;
            _context.Cereals.Remove(Cereal);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
