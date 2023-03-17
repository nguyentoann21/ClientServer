using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    public interface ICartServices
    {
        void AddItem(Product product, int quantity);
        void RemoveItem(Product product);
        void Clear();
        List<CartItem> GetItems();
        decimal GetTotal();
    }
}
