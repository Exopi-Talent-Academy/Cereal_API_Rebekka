using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cereal_API.Models;
using Cereal_API.Repositories;

namespace Cereal_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CerealController : ControllerBase
{
    private readonly ICerealRepository _cerealRepository;

    public CerealController(ICerealRepository cerealRepository)
    {
        _cerealRepository = cerealRepository;
    }

    // GET: api/Cereal
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cereal>>> GetCereals()
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

        /* code given by template, should probably be moved to CerealRepository
        return await _context.Cereals.ToListAsync();
        */
    }

    // GET: api/Cereal/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Cereal>> GetCereal(Guid id)
    {
        try
        {
            var cereal = await _cerealRepository.GetCerealById(id);
            return cereal;
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

        /* code given by template, should probably be moved to CerealRepository
        var cereal = await _context.Cereals.FindAsync(id);

        if (cereal == null)
        {
            return NotFound();
        }

        return cereal;*/
    }

    // PUT: api/Cereal/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost("{id}")]
    public async Task<IActionResult> PostCereal(Guid id, Cereal cereal)
    {
        throw new NotImplementedException();
        // all code given by template, should probably be moved to CerealRepository
        //if (id != cereal.Id)
        //{
        //    return BadRequest();
        //}

        //_context.Entry(cereal).State = EntityState.Modified;

        //try
        //{
        //    await _context.SaveChangesAsync();
        //}
        //catch (DbUpdateConcurrencyException)
        //{
        //    if (!CerealExists(id))
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        throw;
        //    }
        //}

        //return NoContent();
    }

    // POST: api/Cereal
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Cereal>> PostCereal(Cereal cereal)
    {
        throw new NotImplementedException();
        // all code given by template, should probably be moved to CerealRepository
        //_context.Cereals.Add(cereal);
        //await _context.SaveChangesAsync();

        //return CreatedAtAction("GetCereal", new { id = cereal.Id }, cereal);
    }

    // DELETE: api/Cereal/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCereal(Guid id)
    {
        throw new NotImplementedException();
        // all code given by template, should probably be moved to CerealRepository
        //var cereal = await _context.Cereals.FindAsync(id);
        //if (cereal == null)
        //{
        //    return NotFound();
        //}

        //_context.Cereals.Remove(cereal);
        //await _context.SaveChangesAsync();

        //return NoContent();
    }

    // all code given by template, should probably be moved to CerealRepository
    private bool CerealExists(Guid id)
    {
        throw new NotImplementedException();
        //return _context.Cereals.Any(e => e.Id == id);
    }
}
