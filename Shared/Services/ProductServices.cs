using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Shared.DataAccess;
using Shared.Interfaces;
using Shared.Models;

namespace Shared.Services
{
    public class ProductServices : IProductServices
    {
        private readonly AppDbContext _db;

        public ProductServices(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Product product)
        {
            _db.Products.Add(product);
            var result = await _db.SaveChangesAsync();
            return Convert.ToBoolean(result);
        }

        public async Task<bool> Delete(Product product)
        {
            _db.Products.Remove(product);
            var result = await _db.SaveChangesAsync();
            return Convert.ToBoolean(result);
        }

        public async Task<List<Product>> GetAll()
        {
            var result = await _db.Products.Include(x => x.Manufacturers).ToListAsync();
            return result;
        }

        public async Task<Product> GetSingleByID(int id)
        {
            var result = await _db.Products.Include(x => x.Manufacturers).FirstOrDefaultAsync(x => x.ProductID == id);
            return result;
        }

        public async Task<bool> Update(Product product)
        {
            _db.Products.Update(product);
            var result = await _db.SaveChangesAsync();
            return Convert.ToBoolean(result);
        }

        public async Task<IEnumerable<Product>> GetProductByManufacturer(string manufacturer)
        {
            return await _db.Products.Where(p => p.ManufacturerID == manufacturer).ToListAsync();
        }

        public Product GetByID(int productId)
        {
            return _db.Products.FirstOrDefault(p => p.ProductID == productId);
        }

        public async Task<IEnumerable<Product>> SearchByName(string name)
        {
            var product = await _db.Products.Where(x => x.ProductName.Contains(name)).ToListAsync();
            return product;
        }
    }
}
