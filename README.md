 Order Processing with Background Jobs

When a customer places an order:

- Stock is validated.
- Order total is calculated.
- Discount is applied.
- Stock is reserved.
- Order is saved with **Pending** status.
- Customer is redirected immediately without waiting.

Technologies Used

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Hangfire
- Bootstrap 5
- Razor Views



 Features

- Product Listing
- Checkout Page
- Customer Order Placement
- Stock Validation
- Discount Calculation
- Background Order Processing
- Order Status Tracking
- Hangfire Dashboard
- Idempotent Order Processing

 
Order is placed.

Immediately:

- Stock is checked.
- Total is calculated.
- Discount is applied.
- Stock is reduced.
- Order is saved as **Pending**.


 Background Job

Hangfire is used to process orders asynchronously.

The background job performs the following steps:

1. Find the order.
2. Change status to Processing.
3. Simulate payment processing.
4. Simulate confirmation.
5. Mark order as Completed.

Discount Rule

This project includes a simple discount rule.

Example:

- If total amount is greater than ₹1000
- Customer gets 10% discount

This discount is calculated before saving the order.





I used Hangfire because:

- Easy background job scheduling
- Reliable retry mechanism
- Built-in dashboard
- Easy integration with ASP.NET Core
- Good for long-running tasks

Hangfire also allows monitoring of background jobs through its dashboard.


Hangfire Dashboard
It shows:

- Enqueued Jobs
- Processing Jobs
- Completed Jobs
- Failed Jobs
- Retry History


 Database

The project uses SQL Server with Entity Framework Core.

Main tables:

- Products
- Orders
- OrderItems

i learn hangfire from youtube and document then uses in this project



Conclusion

This project demonstrates how background processing can improve the user experience by allowing customers to place orders instantly while lengthy tasks such as payment processing and confirmation are handled asynchronously. It also ensures safe processing through idempotency, preventing duplicate execution of the same order.
