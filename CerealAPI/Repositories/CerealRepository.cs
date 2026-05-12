using Cereal_API.Models;
using Cereal_API.Models.Types;
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

    public async Task<IEnumerable<Cereal>> GetFilteredCereals(string category, OperatorType operation, string value)
    {
        var propertyInfo = typeof(Cereal).GetProperty(category);
        if (propertyInfo == null)
        {
            throw new Exception($"Category '{category}' does not exist.");
        }

        // This big if-block handles strings and the two enums that are essentially strings
        if (propertyInfo.PropertyType == typeof(string) || propertyInfo.PropertyType == typeof(Manufacturers) || propertyInfo.PropertyType == typeof(HotOrColdType))
        {
            if (operation != OperatorType.Equals && operation != OperatorType.NotEquals)
            {
                throw new Exception($"Invalid operator for '{category}' category. Only = and != are allowed.");
            }

            if (propertyInfo.PropertyType != typeof(string))
            {
                if (!Enum.TryParse(propertyInfo.PropertyType, value, true, out var enumValue))
                {
                    throw new Exception($"Invalid value for '{category}' category. Allowed values are: {string.Join(", ", Enum.GetNames(propertyInfo.PropertyType))}.");
                }

                if (propertyInfo.PropertyType == typeof(Manufacturers))
                {
                    var mfr = default(Manufacturers);

                    try
                    {
                        mfr = (Manufacturers)enumValue;
                    }
                    catch (Exception)
                    {
                        throw new Exception($"Invalid value for '{category}' category. Allowed values are: {string.Join(", ", Enum.GetNames(typeof(Manufacturers)))}.");
                    }

                    return operation switch
                    {
                        OperatorType.Equals => await _context.Cereals.Where(c => EF.Property<Manufacturers>(c, category) == mfr).ToListAsync(),
                        OperatorType.NotEquals => await _context.Cereals.Where(c => EF.Property<Manufacturers>(c, category) != mfr).ToListAsync(),
                        _ => throw new Exception("Invalid operator.")
                    };
                }
                else if (propertyInfo.PropertyType == typeof(HotOrColdType))
                {
                    var type = default(HotOrColdType);

                    try
                    {
                        type = (HotOrColdType)enumValue;
                    }
                    catch (Exception)
                    {
                        throw new Exception($"Invalid value for '{category}' category. Allowed values are: {string.Join(", ", Enum.GetNames(typeof(HotOrColdType)))}.");
                    }

                    return operation switch
                    {
                        OperatorType.Equals => await _context.Cereals.Where(c => EF.Property<HotOrColdType>(c, category) == type).ToListAsync(),
                        OperatorType.NotEquals => await _context.Cereals.Where(c => EF.Property<HotOrColdType>(c, category) != type).ToListAsync(),
                        _ => throw new Exception("Invalid operator.")
                    };
                }
            }

            // this is just for the name for now and I'm pretty sure it only allows a case-insensitive exact match. Should probably be more flexible than that
            return operation switch
            {
                OperatorType.Equals => await _context.Cereals.Where(c => EF.Property<string>(c, category).Equals(value, StringComparison.OrdinalIgnoreCase)).ToListAsync(),
                OperatorType.NotEquals => await _context.Cereals.Where(c => !EF.Property<string>(c, category).Equals(value, StringComparison.OrdinalIgnoreCase)).ToListAsync(),
                _ => throw new Exception("Invalid operator.")
            };
        }


        // After this block, we go through the remainder which are all numeric types or integer-based enums


        if (propertyInfo.PropertyType == typeof(VitaminAndMineralsType))
        {
            // should it also accept doubles and have that comparison happen?
            if (!int.TryParse(value, out var intVal))
            {
                throw new Exception($"Invalid value for '{category}' category. Allowed values are integers only.");
            }

            return operation switch
            {
                OperatorType.Equals => await _context.Cereals.Where(c => EF.Property<VitaminAndMineralsType>(c, category).ToInt() == intVal).ToListAsync(),
                OperatorType.NotEquals => await _context.Cereals.Where(c => EF.Property<VitaminAndMineralsType>(c, category).ToInt() != intVal).ToListAsync(),
                OperatorType.GreaterThan => await _context.Cereals.Where(c => EF.Property<VitaminAndMineralsType>(c, category).ToInt() > intVal).ToListAsync(),
                OperatorType.SmallerThan => await _context.Cereals.Where(c => EF.Property<VitaminAndMineralsType>(c, category).ToInt() < intVal).ToListAsync(),
                OperatorType.GreaterThanOrEqual => await _context.Cereals.Where(c => EF.Property<VitaminAndMineralsType>(c, category).ToInt() >= intVal).ToListAsync(),
                OperatorType.SmallerThanOrEqual => await _context.Cereals.Where(c => EF.Property<VitaminAndMineralsType>(c, category).ToInt() <= intVal).ToListAsync(),
                _ => throw new Exception("Invalid operator.")
            };
        }

        if (propertyInfo.PropertyType == typeof(DisplayShelfType))
        {
            // should it also accept doubles and have that comparison happen?
            if (!int.TryParse(value, out var intVal))
            {
                throw new Exception($"Invalid value for '{category}' category. Allowed values are integers only.");
            }

            return operation switch
            {
                OperatorType.Equals => await _context.Cereals.Where(c => EF.Property<DisplayShelfType>(c, category).ToInt() == intVal).ToListAsync(),
                OperatorType.NotEquals => await _context.Cereals.Where(c => EF.Property<DisplayShelfType>(c, category).ToInt() != intVal).ToListAsync(),
                OperatorType.GreaterThan => await _context.Cereals.Where(c => EF.Property<DisplayShelfType>(c, category).ToInt() > intVal).ToListAsync(),
                OperatorType.SmallerThan => await _context.Cereals.Where(c => EF.Property<DisplayShelfType>(c, category).ToInt() < intVal).ToListAsync(),
                OperatorType.GreaterThanOrEqual => await _context.Cereals.Where(c => EF.Property<DisplayShelfType>(c, category).ToInt() >= intVal).ToListAsync(),
                OperatorType.SmallerThanOrEqual => await _context.Cereals.Where(c => EF.Property<DisplayShelfType>(c, category).ToInt() <= intVal).ToListAsync(),
                _ => throw new Exception("Invalid operator.")
            };
        }

        if (propertyInfo.PropertyType == typeof(int))
        {
            // should it also accept doubles and have that comparison happen?
            if (!int.TryParse(value, out var intVal))
            {
                throw new Exception($"Invalid value for '{category}' category. Allowed values are integers only.");
            }

            return operation switch
            {
                OperatorType.Equals => await _context.Cereals.Where(c => EF.Property<int>(c, category) == intVal).ToListAsync(),
                OperatorType.NotEquals => await _context.Cereals.Where(c => EF.Property<int>(c, category) != intVal).ToListAsync(),
                OperatorType.GreaterThan => await _context.Cereals.Where(c => EF.Property<int>(c, category) > intVal).ToListAsync(),
                OperatorType.SmallerThan => await _context.Cereals.Where(c => EF.Property<int>(c, category) < intVal).ToListAsync(),
                OperatorType.GreaterThanOrEqual => await _context.Cereals.Where(c => EF.Property<int>(c, category) >= intVal).ToListAsync(),
                OperatorType.SmallerThanOrEqual => await _context.Cereals.Where(c => EF.Property<int>(c, category) <= intVal).ToListAsync(),
                _ => throw new Exception("Invalid operator.")
            };
        }

        if (propertyInfo.PropertyType == typeof(double))
        {
            if (!double.TryParse(value, out var doubleVal))
            {
                throw new Exception($"Invalid value for '{category}' category. Allowed values are doubles only.");
            }

            return operation switch
            {
                OperatorType.Equals => await _context.Cereals.Where(c => EF.Property<double>(c, category) == doubleVal).ToListAsync(),
                OperatorType.NotEquals => await _context.Cereals.Where(c => EF.Property<double>(c, category) != doubleVal).ToListAsync(),
                OperatorType.GreaterThan => await _context.Cereals.Where(c => EF.Property<double>(c, category) > doubleVal).ToListAsync(),
                OperatorType.SmallerThan => await _context.Cereals.Where(c => EF.Property<double>(c, category) < doubleVal).ToListAsync(),
                OperatorType.GreaterThanOrEqual => await _context.Cereals.Where(c => EF.Property<double>(c, category) >= doubleVal).ToListAsync(),
                OperatorType.SmallerThanOrEqual => await _context.Cereals.Where(c => EF.Property<double>(c, category) <= doubleVal).ToListAsync(),
                _ => throw new Exception("Invalid operator.")
            };
        }

        // it should never reach this point because all property types are covered
        throw new Exception($"Dude, how did you get here? Seriously.");
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
