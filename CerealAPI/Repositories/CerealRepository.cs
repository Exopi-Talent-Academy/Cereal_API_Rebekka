using Cereal_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cereal_API.Repositories;

public class CerealRepository : ICerealRepository
{
    private readonly CerealDbContext _context;

    /// <summary>
    /// An instance of the CerealRepository class is initialized with a CerealDbContext, which is used to interact with the database for performing CRUD operations on cereal data.
    /// </summary>
    /// <param name="context">The CerealDbContext used to access the database. Cannot be null.</param>
    public CerealRepository(CerealDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves a cereal entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cereal to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the cereal entity with the specified
    /// identifier.</returns>
    /// <exception cref="Exception">Thrown if a cereal with the specified identifier is not found.</exception>
    public async Task<Cereal> GetCerealById(Guid id)
    {
        var cereal = await _context.Cereals.FindAsync(id);

        if (cereal == null)
        {
            throw new Exception($"Cereal with ID {id} not found.");
        }

        return cereal;
    }

    public async Task<Cereal> GetCerealByName(string name)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Asynchronously retrieves all available cereals.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of all cereals.</returns>
    public async Task<IEnumerable<Cereal>> GetAllCereals()
    {
        var cereals = await _context.Cereals.ToListAsync();

        if (cereals == null || cereals.Count == 0)
        {
            throw new Exception("No cereals found.");
        }

        return cereals;
    }

    public async Task<IEnumerable<Cereal>> GetSortedCereals(string operatorAndValue, string category)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Creates a new cereal entry in the data store.
    /// </summary>
    /// <param name="cereal">The cereal to add. The object's properties, except for the identifier, are used to create the new entry.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the newly created cereal, including
    /// its generated identifier.</returns>
    public async Task<Cereal> CreateCereal(Cereal cereal)
    {
        // should we check if the cereal already exists by name? Perhaps a future thing

        Cereal newCereal = cereal with { Id = Guid.NewGuid() };

        _context.Cereals.Add(newCereal);
        await _context.SaveChangesAsync();

        return newCereal;
    }

    /// <summary>
    /// Updates the properties of an existing cereal with the specified values.
    /// </summary>
    /// <param name="id">The unique identifier of the cereal to update.</param>
    /// <param name="cereal">The cereal object containing the updated values to apply.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated cereal object.</returns>
    /// <exception cref="Exception">Thrown if a cereal with the specified identifier does not exist.</exception>
    public async Task<Cereal> UpdateCereal(Guid id, Cereal cereal)
    {
        Cereal trackedCereal;
        
        try
        {
            trackedCereal = GetCerealById(id).Result;
        } 
        catch (Exception)
        {
            throw;
        }

        _context.Entry(trackedCereal).CurrentValues.SetValues(cereal);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CerealExists(id))
            {
                throw new Exception("Cereal not found.");
            }
            else
            {
                throw;
            }
        }

        return cereal;
    }

    /// <summary>
    /// Deletes the cereal with the specified unique identifier from the data store.
    /// </summary>
    /// <param name="id">The unique identifier of the cereal to delete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the cereal was
    /// successfully deleted.</returns>
    /// <exception cref="Exception">Thrown if a cereal with the specified <paramref name="id"/> does not exist, or if an error occurs while deleting
    /// the cereal.</exception>
    public async Task<bool> DeleteCereal(Guid id)
    {
        var cereal = await _context.Cereals.FindAsync(id);
        if (cereal == null)
        {
            throw new Exception($"Cereal with ID {id} not found.");
        }

        try
        {
            _context.Cereals.Remove(cereal);
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }

        return true;
    }

    /// <summary>
    /// Determines whether a cereal with the specified unique identifier exists in the database.
    /// </summary>
    /// <param name="id">The unique identifier of the cereal to locate.</param>
    /// <returns>true if a cereal with the specified identifier exists; otherwise, false.</returns>
    public bool CerealExists(Guid id)
    {
        return _context.Cereals.Any(e => e.Id == id);
    }
}
