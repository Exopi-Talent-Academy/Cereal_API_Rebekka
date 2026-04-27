using Cereal_API.Models;

namespace Cereal_API.Repositories;

public class CerealRepository : ICerealRepository
{
    public async Task<CerealDTO> GetCerealById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<CerealDTO> GetCerealByName(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CerealDTO>> GetAllCereals()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CerealDTO>> GetSortedCereals(string predicate, string category)
    {
        throw new NotImplementedException();
    }

    public async Task<CerealDTO> CreateCereal(CerealDTO cereal)
    {
        throw new NotImplementedException();
    }

    public async Task<CerealDTO> UpdateCereal(Guid id, CerealDTO cereal)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteCereal(Guid id)
    {
        throw new NotImplementedException();
    }
}
