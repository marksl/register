namespace Register
{
    public interface IItemPrices
    {
        decimal GetPrice(ItemId id);
        decimal GetWeighedPrice(ItemId id);
    }
}