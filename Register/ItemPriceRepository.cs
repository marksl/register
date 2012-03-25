using System.Collections.Generic;

namespace Register
{
    public class ItemPriceRepository
    {
        public ItemPriceRepository()
        {
            _items = new Dictionary<Item, decimal>();
        }

        public void AddItem(Item item, decimal itemPrice)
        {
            
        }

        public void SetBulkDiscount(Item item, BulkDiscount bulkDiscount)
        {

        }
        
        private Dictionary<Item, decimal> _items;

        public IEnumerable<Item> RemoveBulkDiscountedItems(List<Item> items)
        {
            throw new System.NotImplementedException();
        }
    }
}