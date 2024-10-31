namespace DineConnect.OrderManagementService.Contracts.Order
{
    public record OrderSummaryResponse(
                 string Id,
                 string OrderStatus, List<OrderItemResponse> MenuItems)
    {
        public OrderSummaryResponse(string Id,
             string OrderStatus) : this(Id, OrderStatus, new List<OrderItemResponse>()) { }
    }

}
