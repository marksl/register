using System.Collections.Generic;

namespace Register
{
    public interface IDiscounts
    {
        decimal GetTotalAfterDiscounts(IEnumerable<Item> items, decimal total);
    }
}