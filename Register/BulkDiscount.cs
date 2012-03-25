using System;

namespace Register
{
    public class BulkDiscount
    {
        private readonly ItemId _itemId;
        private readonly int _numberRequiredToBuy;
        private readonly int _numberReceivedFree;

        private int numberBought;
        private int numberFree;

        public BulkDiscount(ItemId itemId, int buyX, int getYFree)
        {
            if (buyX < 2)
            {
                throw new InvalidOperationException("Must specify atleast 2 required.");
            }

            if (getYFree < 1)
            {
                throw new InvalidOperationException("Must specify atleast 1 received free.");
            }

            _itemId = itemId;
            _numberRequiredToBuy = buyX - getYFree;
            _numberReceivedFree = getYFree;
        }

        internal ItemId ItemId
        {
            get { return _itemId; }
        }

        internal BulkDiscount Clone()
        {
            return new BulkDiscount(_itemId, _numberRequiredToBuy, _numberReceivedFree);
        }

        internal bool IsNextItemFree(Item item)
        {
            if (numberFree > 0)
            {
                numberFree--;

                if (numberFree == 0)
                {
                    numberBought = 0;
                }

                return true;
            }

            numberBought++;
            if (numberBought > _numberRequiredToBuy)
            {
                numberFree = _numberReceivedFree;
            }

            return false;
        }
    }
}