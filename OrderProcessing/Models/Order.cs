using OrderProcessing.Models;

namespace OrderProcessing.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = "";

        public decimal Total { get; set; }

        public decimal DiscountAmount { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? ProcessedAt { get; set; }

        public List<OrderItem> Items { get; set; } = new();
    }
}