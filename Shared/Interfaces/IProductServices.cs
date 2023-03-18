using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    public interface IProductServices
    {
        Task<List<Product>> GetAll(int numberPage, int sizePage);

        Task<Product> GetSingleByID(int id);

        Task<bool> Create(Product product);

        Task<bool> Update(Product product);

        Task<bool> Delete(Product product);

        Task<IEnumerable<Product>> GetProductByManufacturer(string manufacturer);

        Product GetByID(int id);

        Task<IEnumerable<Product>> SearchByName(string name);

    }
}
