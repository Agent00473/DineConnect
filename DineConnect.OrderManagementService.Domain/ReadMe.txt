    Contains Aggregates, Entities, and Value Objects
    Aggregate refer other Aggregates using Aggregate Id.

Aggregates
Order: An aggregate that includes order-related information
   -Entities: 
     - MenuItem : 1..* (Items in the order)
     - Payment  (Details of the payment for the order)
   -Value Objects:
     - OrderStatus (Enum representing the order's status)
     - RestaurentID (Restaurent Order associated with) 
     - OrderId
     - CustomerId  (Customer Order associated with) 
  
Restaurant
  -Value Objects:
     - RestaurentID
     - OrderId : 1..*

Customer
   -Entities: 
      DeliveryAddress (Customer's delivery address)
   -Value Objects:
     - CustomerId

Entities
   -DeliveryAddress: Represents an item in an order.
   -Payment: Represents the payment details for an order.
   -MenuItem: Represents an individual menu item within a restaurant.

Value Objects
    -OrderStatus:  Enum representing the different statuses an order can have (e.g., Pending, Confirmed, etc.).
    -PaymentStatus: Enum representing the status of a payment (e.g., Pending, Completed, etc.).
    -DeliveryAddress: A value object that represents the address for delivery.

