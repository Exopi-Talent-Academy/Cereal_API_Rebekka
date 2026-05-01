using Cereal_API.Models;

namespace Cereal_API.Repositories;

public interface ICerealRepository
{
    Task<Cereal> GetCerealById(Guid id);
    Task<Cereal> GetCerealByName(string name);
    Task<IEnumerable<Cereal>> GetAllCereals();
    Task<IEnumerable<Cereal>> GetSortedCereals(string operatorAndValue, string category);
    Task<Cereal> CreateCereal(Cereal cereal);
    Task<Cereal> UpdateCereal(Guid id, Cereal cereal);
    Task<bool> DeleteCereal(Guid id);
}
