namespace OrderProcessing.Services
{
    public interface IOrderProcessingService
    {
        Task ProcessOrderAsync(int orderId);
    }
}
