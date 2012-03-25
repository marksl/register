using System;

namespace Register
{
    public class Coupon
    {
        private readonly decimal _moneyOff;
        private readonly decimal _threshold;

        public Coupon(decimal moneyOff, decimal threshold)
        {
            if (moneyOff <= 0.0M)
            {
                throw new InvalidOperationException("moneyOff must be greater than $0.00.");
            }

            if (threshold <= 0.0M)
            {
                throw new InvalidOperationException("threshold must be greater than $0.00.");
            }

            _moneyOff = moneyOff;
            _threshold = threshold;
        }

        internal decimal GetTotal(decimal total)
        {
            return total > _threshold ? total - _moneyOff : total;
        }
    }
}