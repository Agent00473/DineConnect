namespace DineConnect.OrderManagementService.Contracts.Restaurant
{
    public record RestaurantResponse(string Id, string name, IList<string> OrderIds)
    {
        public RestaurantResponse(string Id, string Name) : this(Id, Name, new List<string>()) { }
    }
}
