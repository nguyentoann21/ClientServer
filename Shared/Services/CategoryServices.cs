using Microsoft.EntityFrameworkCore;
using Shared.DataAccess;
using Shared.Interfaces;
using Shared.Models;

namespace Shared.Services
{
    public class CategoryServices : ICaregoryServices
    {
        private readonly AppDbContext _db;

        public CategoryServices(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Manufacturer manufacturer)
        {
            _db.Manufacturers.Add(manufacturer);
            var result = await _db.SaveChangesAsync();
            return Convert.ToBoolean(result);
        }

        public async Task<bool> Delete(Manufacturer manufacturer)
        {
            _db.Manufacturers.Remove(manufacturer);
            var result = await _db.SaveChangesAsync();
            return Convert.ToBoolean(result);
        }

        public async Task<List<Manufacturer>> GetAll()
        {
            var result = await _db.Manufacturers.ToListAsync();
            return result;
        }

        public async Task<Manufacturer> GetSingleByID(string id)
        {
            var result = await _db.Manufacturers.FirstOrDefaultAsync(x => x.ManufacturerID == id);
            return result;
        }

        public async Task<bool> Update(Manufacturer manufacturer)
        {
            _db.Manufacturers.Update(manufacturer);
            var result = await _db.SaveChangesAsync();
            return Convert.ToBoolean(result);
        }
    }
}
