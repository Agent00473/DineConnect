using Infrastructure.IntegrationEvents.Entities.Events;
namespace InfraTest.Events
{

    public static class EventGenerator
    {
        private static readonly Random Random = new();

        private static readonly List<string> CustomerNames = new()
    {
        "John Smith", "Jane Doe", "Alice Johnson", "Bob Brown", "Charlie White"
    };

        private static readonly List<string> CustomerEmails = new()
    {
        "john.smith@example.com", "jane.doe@example.com", "alice.j@example.com", "bob.b@example.com", "charlie.w@example.com"
    };

        private static readonly List<string> OrderNames = new()
    {
        "Electronics", "Groceries", "Clothing", "Books", "Furniture"
    };

        public static List<IntegrationEvent> GenerateRandomEvents()
        {
            int eventCount = Random.Next(5, 20); // Generate between 5 and 20 events
            var events = new List<IntegrationEvent>();

            for (int i = 0; i < eventCount; i++)
            {
                bool isCustomerEvent = Random.Next(0, 2) == 0; // Randomly decide event type

                if (isCustomerEvent)
                {
                    var randomName = CustomerNames[Random.Next(CustomerNames.Count)];
                    var randomEmail = CustomerEmails[Random.Next(CustomerEmails.Count)];
                    var randomAction = (EventActionCategory)Random.Next(0, 3);

                    events.Add(new CustomerIntegrationEvent(Guid.NewGuid(), randomName, randomEmail, randomAction));
                }
                else
                {
                    var randomOrderName = OrderNames[Random.Next(OrderNames.Count)];
                    var randomCustomerId = Guid.NewGuid(); // Random customer ID for the order
                    var randomAction = (EventActionCategory)Random.Next(0, 3);

                    events.Add(new OrderEvent(Guid.NewGuid(), randomCustomerId, randomOrderName, randomAction));
                }
            }

            return events;
        }
    }


}
