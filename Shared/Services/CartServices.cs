using Shared.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    public class CartServices : ICartServices
    {
        private readonly List<CartItem> _items = new List<CartItem>();

        public void AddItem(Product product, int quantity)
        {
            var item = _items.FirstOrDefault(i => i.Product.ProductID == product.ProductID);

            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                _items.Add(new CartItem
                {
                    Product = product,
                    Quantity = quantity
                });
            }
        }

        public void RemoveItem(Product product)
        {   
            var item = _items.FirstOrDefault(i => i.Product.ProductID == product.ProductID);

            if (item != null)
            {
                _items.Remove(item);
            }
        }

        public void Clear()
        {
            _items.Clear();
        }

        public List<CartItem> GetItems()
        {
            return _items;
        }

        public decimal GetTotal()
        {
            return _items.Sum(i => i.Product.ProductPrice * i.Quantity);
        }
    }
}
