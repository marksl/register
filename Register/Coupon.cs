namespace Register
{
    public abstract class Coupon
    {
        protected Coupon(ItemId itemId)
        {
            ItemId = itemId;
        }

        public ItemId ItemId { get; private set; }

        internal decimal Apply(decimal itemTotal)
        {
            throw new System.NotImplementedException();
        }
    }
}