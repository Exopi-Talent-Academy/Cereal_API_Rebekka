using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cereal_API.Models;
using Cereal_API.Repositories;

namespace Cereal_API.Pages.Cereal;

public class ListCerealsModel : PageModel
{
    private readonly ICerealRepository _repository;

    public ListCerealsModel(ICerealRepository repository)
    {
        _repository = repository;
    }

    public IList<Models.Cereal> Cereal { get;set; } = default!;

    public async Task OnGetAsync()
    {
        var ok = await _repository.GetAllCereals();
        Cereal = ok.OrderBy(c => c.Name).ToList();
    }
}
