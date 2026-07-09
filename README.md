# OrderProcessing

## Project Name
Order Processing with Background Jobs


# Requirement Analysis

I carefully studied the project requirements to understand the complete workflow.

The application should allow customers to place orders instantly while the actual processing (payment simulation and order confirmation) should happen asynchronously in the background using Hangfire.

The project also requires:

- Product Management
- Order Management
- Order Items
- Stock Validation
- Discount Calculation
- Background Job Processing
- Idempotency
- Order Status Tracking


# Folder Structure Created

The following folders were created.

│
├── Controllers
├── Models
├── Data
├── Repositories
├── IRepositories
├── Services
├── IServices
├── ViewModels
├── Views
├── wwwroot


# Models Created

The following entity classes were created.

### Product

Stores product information.

Properties

- Id
- Name
- Price
- Stock


### Order

Stores customer order information.

Properties

- Id
- CustomerName
- Total
- DiscountAmount
- Status
- CreatedAt
- ProcessedAt


### OrderItem

Stores products belonging to an order.

Properties

- Id
- OrderId
- ProductId
- Quantity
- UnitPrice


