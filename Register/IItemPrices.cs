namespace Register
{
    public interface IItemPrices
    {
        decimal GetPrice(ItemId itemId);
        decimal GetWeighedPrice(ItemId itemId);
    }
}