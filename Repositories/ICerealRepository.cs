using Cereal_API.Models;

namespace Cereal_API.Repositories;

public interface ICerealRepository
{
    Task<CerealDTO> GetCerealById(Guid id);
    Task<CerealDTO> GetCerealByName(string name);
    Task<IEnumerable<CerealDTO>> GetAllCereals();
    Task<IEnumerable<CerealDTO>> GetSortedCereals(string predicate, string category);
    Task<CerealDTO> CreateCereal(CerealDTO cereal);
    Task<CerealDTO> UpdateCereal(Guid id, CerealDTO cereal);
    Task<bool> DeleteCereal(Guid id);
}
