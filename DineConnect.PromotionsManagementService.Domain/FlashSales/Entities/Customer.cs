using DineConnect.PromotionsManagementService.Domain.Common;
using DineConnect.PromotionsManagementService.Domain.FlashSales.ValueObjects;

namespace DineConnect.PromotionsManagementService.Domain.FlashSales.Entities
{
    public class Customer: BaseEntity<Guid>
    {
        private Customer() : base()
        {
            Id = Guid.NewGuid();
            Type = CustomerType.Regular; // Default customer type (can be changed as needed)
            Location = string.Empty;  // Default empty location
            IsNewCustomer = false;  // Default assumption for new customers
            SpecialOccasionDate = null;  // No special occasion by default
        }

        // Private constructor for creation with an existing ID
        private Customer(Guid id):  base(id)
        {
            Type = CustomerType.Regular;  // Default customer type (can be changed as needed)
            Location = string.Empty;  // Default empty location
            IsNewCustomer = false;  // Default assumption for new customers
            SpecialOccasionDate = null;  // No special occasion by default
        }
        public Guid CustomerId { get; set; }
        public CustomerType Type { get; set; } 
        public string Location { get; set; }
        public bool IsNewCustomer { get; set; }
        public DateTime? SpecialOccasionDate { get; set; } 
        
        public static Customer Create()
        {
            return new Customer();
        }
        
        public static Customer Create(Guid id, CustomerType type = CustomerType.Regular, string location = "", bool isNewCustomer = false, DateTime? specialOccasionDate = null)
        {
            var customer = new Customer(id)
            {
                Type = type,
                Location = location,
                IsNewCustomer = isNewCustomer,
                SpecialOccasionDate = specialOccasionDate
            };
            return customer;
        }

    }

}
