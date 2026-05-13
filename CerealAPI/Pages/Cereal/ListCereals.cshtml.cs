using Cereal_API.Controllers;
using Cereal_API.Models.Types;
using Cereal_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cereal_API.Pages.Cereal;

public class ListCerealsModel : PageModel
{
    private readonly CerealController _controller;

    public ListCerealsModel(ICerealRepository repository)
    {
        _controller = new CerealController(repository);
    }

    public IList<Models.Cereal> Cereal { get;set; } = default!;
    private string FilterCategory { get; set; } = string.Empty;
    private OperatorType FilterOperation { get; set; }
    private string FilterValue { get; set; } = string.Empty;
    // should have something that determines how it gets sorted

    public async Task OnGetAsync()
    {
        ActionResult<IEnumerable<Models.Cereal>> ok;
        
        if (FilterCategory == string.Empty || FilterValue == string.Empty)
        {
            ok = await _controller.GetCereals();
        } 
        else
        {
            ok = await _controller.GetCereals(FilterCategory, FilterOperation, FilterValue);
        }

        var cereals = (ok.Result as ViewResult)?.Model as IEnumerable<Models.Cereal>;
        Cereal = cereals!.OrderBy(c => c.Name).ToList();
    }

    public async Task FilterCereals(string category, OperatorType operation, string value)
    {
        if (typeof(Models.Cereal).GetProperty(category) == null) { return; }

        if (value != string.Empty)
        {
            FilterCategory = category;
            FilterOperation = operation;
            FilterValue = value;

            await OnGetAsync();
        }
    }

    public async Task ResetFilter()
    {
        FilterCategory = string.Empty;
        FilterValue = string.Empty;

        await OnGetAsync();
    }
}
