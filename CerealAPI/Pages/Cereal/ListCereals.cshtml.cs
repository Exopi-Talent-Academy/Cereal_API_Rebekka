using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        //should take some inputs that determines how the cereals are sorted, filtered, etc. but for now just return all cereals ordered by name
        var ok = await _controller.GetCereals();
        var cereals = (ok.Result as ViewResult)?.Model as IEnumerable<Models.Cereal>;
        Cereal = cereals!.OrderBy(c => c.Name).ToList();
    }
}
