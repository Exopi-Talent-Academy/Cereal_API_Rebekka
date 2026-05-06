using Cereal_API.Models;

namespace Cereal_API.Repositories;

public class CerealRepository : ICerealRepository
{
    private readonly CerealDbContext _context;

    public CerealRepository(CerealDbContext context)
    {
        _context = context;
    }

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

    public async Task<IEnumerable<Cereal>> GetAllCereals()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Cereal>> GetSortedCereals(string operatorAndValue, string category)
    {
        throw new NotImplementedException();
    }

    public async Task<Cereal> CreateCereal(Cereal cereal)
    {
        throw new NotImplementedException();
    }

    public async Task<Cereal> UpdateCereal(Guid id, Cereal cereal)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteCereal(Guid id)
    {
        throw new NotImplementedException();
    }
}
