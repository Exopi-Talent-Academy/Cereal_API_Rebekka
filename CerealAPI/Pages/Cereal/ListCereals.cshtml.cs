using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cereal_API.Models;
using Cereal_API.Repositories;
using Cereal_API.Controllers;

namespace Cereal_API.Pages.Cereal;

public class ListCerealsModel : PageModel
{
    private readonly CerealController _controller;

    public ListCerealsModel(ICerealRepository repository)
    {
        _controller = new CerealController(repository);
    }

    public IList<Models.Cereal> Cereal { get;set; } = default!;

    public async Task OnGetAsync()
    {
        var ok = await _controller.GetCereals();
        var cereals = (ok.Result as ViewResult)?.Model as IEnumerable<Models.Cereal>;
        Cereal = cereals!.OrderBy(c => c.Name).ToList();
    }
}
