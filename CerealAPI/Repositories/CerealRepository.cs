using Cereal_API.Models;

namespace Cereal_API.Repositories;

public class CerealRepository : ICerealRepository
{
    public async Task<Cereal> GetCerealById(Guid id)
    {
        throw new NotImplementedException();
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
