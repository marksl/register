using System.Collections.Generic;

namespace Register
{
    public interface IDiscounts
    {
        decimal GetDiscount(IEnumerable<Item> items);
    }
}