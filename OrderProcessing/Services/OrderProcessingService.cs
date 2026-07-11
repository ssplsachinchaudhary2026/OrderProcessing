using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using OrderProcessing.Data;
using OrderProcessing.Models;

namespace OrderProcessing.Services
{
    public class OrderProcessingService : IOrderProcessingService
    {
        private readonly ApplicationDbContext _context;
        public OrderProcessingService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task ProcessOrderAsync(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            if (order == null)
                return;
            if (order.Status != OrderStatus.Pending)
                return;
            order.Status = OrderStatus.Processing;
            await _context.SaveChangesAsync();
            await Task.Delay(5000);
        
        
        }
    }
}
