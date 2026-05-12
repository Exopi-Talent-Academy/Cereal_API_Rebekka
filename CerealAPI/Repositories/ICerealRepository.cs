using Cereal_API.Models;
using Cereal_API.Models.Types;

namespace Cereal_API.Repositories;

public interface ICerealRepository
{
    Task<Cereal> GetCerealById(Guid id);
    Task<Cereal> GetCerealByName(string name);
    Task<IEnumerable<Cereal>> GetAllCereals();
    Task<IEnumerable<Cereal>> GetFilteredCereals(string category, OperatorType operation, string value);
    Task<Cereal> CreateCereal(Cereal cereal);
    Task<Cereal> UpdateCereal(Guid id, Cereal cereal);
    Task<bool> DeleteCereal(Guid id);
    bool CerealExists(Guid id);
}
