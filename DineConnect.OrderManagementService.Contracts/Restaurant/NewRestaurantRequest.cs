namespace DineConnect.OrderManagementService.Contracts.Restaurant
{
    public record NewRestaurantRequest(string name,  IList<string> OrderIds )
    {
        public NewRestaurantRequest(string Name) : this(Name, new List<string>()) { }
    }
}
