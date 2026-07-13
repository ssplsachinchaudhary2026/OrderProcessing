using OrderProcessing.Models;

namespace OrderProcessing.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order?> GetOrderWithItemsAsync(int orderId);
    }
}
