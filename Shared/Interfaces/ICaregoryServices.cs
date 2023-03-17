using Shared.Models;

namespace Shared.Interfaces
{
    public interface ICaregoryServices
    {
        Task<List<Manufacturer>> GetAll();
        
        Task<Manufacturer> GetSingleByID(string id);

        Task<bool> Create(Manufacturer manufacturer);

        Task<bool> Update(Manufacturer manufacturer);

        Task<bool> Delete(Manufacturer manufacturer);
    }
}
