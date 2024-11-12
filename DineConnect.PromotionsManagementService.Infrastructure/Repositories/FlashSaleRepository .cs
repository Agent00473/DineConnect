using DineConnect.PromotionsManagementService.Application.Interfaces;
using DineConnect.PromotionsManagementService.Domain.FlashSales.Entities;
using DineConnect.PromotionsManagementService.Domain.FlashSales.ValueObjects;

namespace DineConnect.PromotionsManagementService.Infrastructure.Repositories
{
    //TODO: Implement Redis based Repository for Promotions Management
    public class FlashSaleRepository : IFlashSaleRepository
    {
        public static Guid SaleID = new Guid("6C63C2DA-4A22-4346-8296-0DE700A3BC9B");
        public static Guid CustomerID_1 = new Guid("A18A9013-3FE1-4E21-A6F6-0A5CF5C08FC8");
        public static Guid CustomerID_2 = new Guid("E4DDC41D-9BAC-4933-84E8-053DFE5B5284");

        // For demo purposes, using an in-memory store.
        private readonly List<FlashSale> _flashSales;
        private readonly List<Customer> _customers;
        public FlashSaleRepository()
        {

            _flashSales = new List<FlashSale>();

            var tiers = new List<DiscountTier>
                    {
                        DiscountTier.Create(100, 10,true),
                        DiscountTier.Create(200,20, true )
                    };
            var sale = FlashSale.Create(SaleID, "Holiday Sale", FlashSaleType.Holiday, DateTime.Now.AddDays(-5),
                 DateTime.Now.AddDays(5), "Electronics",string.Empty, tiers);
            _flashSales.Add(sale);

            _customers = new List<Customer>
            {
                Customer.Create(CustomerID_1, CustomerType.Premium, "New York", true, new DateTime(2024, 12, 15)), // Premium customer with special occasion
                Customer.Create(CustomerID_2, CustomerType.Regular, "Los Angeles", false) // Regular customer
            };
        }
        public async Task<FlashSale> GetFlashSaleByIdAsync(Guid flashSaleId)
        {
            // Simulate async data fetching (in reality, you'd query the database).
            return await Task.FromResult(_flashSales.FirstOrDefault(fs => fs.Id == flashSaleId));
        }

        public async Task<FlashSale> GetFlashSaleByTypeAsync(FlashSaleType saleType)
        {
            // Simulate async data fetching.
            return await Task.FromResult(_flashSales.FirstOrDefault(fs => fs.SaleType == saleType));
        }

        public async Task AddFlashSaleAsync(FlashSale flashSale)
        {
            // Simulate async adding (in reality, you'd insert this into the database).
            _flashSales.Add(flashSale);
            await Task.CompletedTask;
        }

        public async Task UpdateFlashSaleAsync(FlashSale flashSale)
        {
            // Find the existing flash sale and update it.
            var existingSale = _flashSales.FirstOrDefault(fs => fs.Id == flashSale.Id);
            if (existingSale != null)
            {
                existingSale.SaleName = flashSale.SaleName;
                existingSale.SaleType = flashSale.SaleType;
                foreach (var item in flashSale.DiscountTiers)
                {
                    existingSale.AddDiscountTier(item);
                }
                existingSale.StartDate = flashSale.StartDate;
                existingSale.EndDate = flashSale.EndDate;
            }

            await Task.CompletedTask;
        }

        public async Task DeleteFlashSaleAsync(Guid flashSaleId)
        {
            var flashSale = _flashSales.FirstOrDefault(fs => fs.Id == flashSaleId);
            if (flashSale != null)
            {
                _flashSales.Remove(flashSale);
            }

            await Task.CompletedTask;
        }

        // Get customer by Id
        public async Task<Customer> GetCustomerAsync(Guid customerId)
        {
            // In a real application, this would be a database call to fetch customer data
            var customer = _customers.FirstOrDefault(c => c.CustomerId == customerId);
            return await Task.FromResult(customer);
        }
    }
}
