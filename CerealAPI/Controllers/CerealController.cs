using Cereal_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cereal_Api.Controllers;

[Route("api/cereal")]
public class CerealController : Controller
{
    private readonly ICerealRepository _cerealRepository;

    public CerealController(ICerealRepository cerealRepository)
    {
        _cerealRepository = cerealRepository;
    }

    [HttpGet]
    async public Task<IActionResult> GetCereal(Guid Id)
    {
        try 
        {
            var cereal = await _cerealRepository.GetCerealById(Id);
            return Ok(cereal);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    async public Task<IActionResult> GetAllCereals()
    {
        try 
        {
            var cereals = await _cerealRepository.GetAllCereals();
            return Ok(cereals);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
