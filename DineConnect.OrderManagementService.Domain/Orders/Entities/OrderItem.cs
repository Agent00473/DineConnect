using DineConnect.OrderManagementService.Domain.Common;
using DineConnect.OrderManagementService.Domain.Orders.ValueObjects;

namespace DineConnect.OrderManagementService.Domain.Orders.Entities
{

    public class OrderItem : BaseEntity<OrderItemId>
    {
        private OrderItem(OrderItemId itemId, string itemName, decimal price) : base(itemId)
        {
            ItemName = itemName;
            Price = price;
        }
        private OrderItem()
        {
        }

        public string ItemName { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        public static OrderItem Create(OrderItemId itemId, string itemName, decimal price)
        {
            return new OrderItem(itemId, itemName, price);
        }
        public static OrderItem Create(string itemName, decimal price)
        {
            return new OrderItem(OrderItemId.Create(), itemName, price);
        }
    }

}
