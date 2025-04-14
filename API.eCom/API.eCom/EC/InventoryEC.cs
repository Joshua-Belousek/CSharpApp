using API.eCom.Database;
using Libary.eCom.Models;

namespace API.eCom.EC
{
    public class InventoryEC
    {

        public List<Item?> Get()
        {
            return FakeDatabase.Inventory;
        }

        public Item? Delete(int id)
        {
            var itemToDelete = FakeDatabase.Inventory.FirstOrDefault(i => i?.Id == id);
            if (itemToDelete != null)
            {
                FakeDatabase.Inventory.Remove(itemToDelete);
            }

            return itemToDelete;
        }

        public Item? AddOrUpdate(Item item)
        {
            if (item.Id == 0)
            {
                item.Id = FakeDatabase.LastKey_Item + 1;
                item.Product.Id = item.Id;
                FakeDatabase.Inventory.Add(item);
            }
            else
            {
                var existingItem = FakeDatabase.Inventory.FirstOrDefault(p => p.Id == item.Id);
                var index = FakeDatabase.Inventory.IndexOf(existingItem);
                FakeDatabase.Inventory.RemoveAt(index);
                FakeDatabase.Inventory.Insert(index, new Item(item));
            }

            return item;
        }

        public Item? buy(int Id, int count)
        {
            if (count == 0) return null;

            var selectedProd = FakeDatabase.Inventory.FirstOrDefault(p => p.Id == Id);

            if (selectedProd == null)
                return null;
            if (selectedProd.Count < count)
                return null;

            Item ShoppingCartProduct = new Item(selectedProd);

            selectedProd.Count -= count;
            ShoppingCartProduct.Count = count;


            return ShoppingCartProduct;
        }

        public Item? Return(int id, int count)
        {
            var item = FakeDatabase.Inventory.FirstOrDefault(p => p.Id == id);
            if (item == null)
                return null;
            item.Count += count;

            return item;
        }
    }
    }
