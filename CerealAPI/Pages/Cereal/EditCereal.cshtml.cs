using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cereal_API.Models;

namespace Cereal_API.Pages.Cereal;

public class EditCerealModel : PageModel
{
    private readonly Cereal_API.Models.CerealDbContext _context;

    public EditCerealModel(Cereal_API.Models.CerealDbContext context)
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

        var cereal =  await _context.Cereals.FirstOrDefaultAsync(m => m.Id == id);
        if (cereal == null)
        {
            return NotFound();
        }
        Cereal = cereal;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Cereal).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CerealExists(Cereal.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool CerealExists(Guid id)
    {
        return _context.Cereals.Any(e => e.Id == id);
    }
}
