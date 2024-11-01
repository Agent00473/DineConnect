namespace DineConnect.OrderManagementService.Contracts.Requests
{
    public record NewRestaurantRequest(string name, IList<string> OrderIds)
    {
        public NewRestaurantRequest(string Name) : this(Name, new List<string>()) { }
    }
}
