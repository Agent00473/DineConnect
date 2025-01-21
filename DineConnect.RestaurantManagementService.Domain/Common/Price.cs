using Infrastructure.Domain.Entities;

namespace DineConnect.RestaurantManagementService.Domain.Common
{
    public class Price : BaseValueObject
    {

        #region Constructors
        private Price(decimal amount, decimal tax, decimal discount)
        {
            Amount = amount;
            Tax = tax;
            Discount = discount;
        }
        #endregion

        #region Public Properties
        public decimal Amount { get; private set; }
        public decimal Discount { get; private set; }
        public decimal Tax { get; private set; }
        #endregion

        #region Public Methods
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Discount;
            yield return Tax;
        }
        #endregion

        #region Factory Methods
        public static Price Create(decimal amount, decimal tax, decimal discount)
        {
            if (amount < 0) throw new ArgumentException("Amount cannot be negative.");
            if (tax < 0) throw new ArgumentException("Tax cannot be negative.");
            if (discount < 0) throw new ArgumentException("Discount cannot be negative.");
            return new Price(amount,tax,discount);
        }

        public static Price Create(decimal amount)
        {
            if (amount < 0) throw new ArgumentException("Amount cannot be negative.");
            return new Price(amount, 0, 0);
        }

        #endregion
    }
}
