using Hangfire;
using Microsoft.EntityFrameworkCore;

using OrderProcessing.Data;
using OrderProcessing.Models;
using OrderProcessing.ViewModels;



namespace OrderProcessing.Services
{
    public class OrderService : IOrderServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public OrderService(ApplicationDbContext context, IBackgroundJobClient backgroundJobClient)
        {
            _context = context;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<CheckoutViewModel> GetCheckoutDataAsync()
        {
            var products = await _context.Products.ToListAsync();

            return new CheckoutViewModel
            {
                Items = products.Select(p => new CheckoutItemViewModel
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    Price = p.Price,
                    Stock = p.Stock,
                    Quantity = 0
                }).ToList()
            };
        }

        public async Task<int> PlaceOrderAsync(CheckoutViewModel model)
        {
            var selectedItems = model.Items.Where(x => x.Quantity > 0).ToList();

            if (!selectedItems.Any())
                throw new Exception("Please select at least one product.");

            var productIds = selectedItems.Select(x => x.ProductId).ToList();
            var products = await _context.Products
                .Where(x => productIds.Contains(x.Id))
                .ToListAsync();

            decimal total = 0;
            var order = new Order
            {
                CustomerName = model.CustomerName,
                Status = OrderStatus.Pending
            };

            foreach (var item in selectedItems)
            {
                var product = products.First(x => x.Id == item.ProductId);

                if (product.Stock < item.Quantity)
                    throw new Exception($"{product.Name} stock is not available.");

                product.Stock = product.Stock - item.Quantity;

                total += product.Price * item.Quantity;

                order.Items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });
            }

            // Discount
            if (total > 10000)
            {
                order.DiscountAmount = total * 0.10m;
            }

            order.Total = total - order.DiscountAmount;

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            // Background job
            _backgroundJobClient.Enqueue<IOrderProcessingService>(
                service => service.ProcessOrderAsync(order.Id));

            return order.Id;
        }
    }
}