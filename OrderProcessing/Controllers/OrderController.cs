using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderProcessing.Data;
using OrderProcessing.Services;
using OrderProcessing.ViewModels;

namespace OrderProcessing.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderServices _orderService;
        private readonly ApplicationDbContext _context;
        public OrderController(IOrderServices orderServices,ApplicationDbContext context)
        {
            _orderService = orderServices;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var model = await _orderService.GetCheckoutDataAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var freshModel = await _orderService.GetCheckoutDataAsync();
                freshModel.CustomerName = model.CustomerName;
                return View(freshModel);

            }
            var products = await _context.Products.ToListAsync();
            var selectedItems = model.Items.Where(x => x.Quantity > 0).ToList();

            if (!selectedItems.Any())
            {
                ModelState.AddModelError("", "Please select at least one product.");
                var freshModel = await _orderService.GetCheckoutDataAsync();
                return View(freshModel);
            }
            foreach (var item in selectedItems)
            {
                var product = products.FirstOrDefault(x => x.Id == item.ProductId);
                item.ProductName = product?.Name ?? "";
                item.Price = product?.Price ?? 0;
                item.Stock = product?.Stock ?? 0;
            }
            model.Items = selectedItems;
            return View("Confirm", model);
        }
        [HttpPost]
        public async Task<IActionResult> Confirm(CheckoutViewModel model)
        {
            try
            {
                var orderId = await _orderService.PlaceOrderAsync(model);
                return RedirectToAction("Status", new { id = orderId });
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var freshModel = await _orderService.GetCheckoutDataAsync();
                return View(freshModel);
            }
}
        public async Task<IActionResult>Status(int id)
        {
            var order = await _context.Orders.Include(x => x.Items).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.Id == id);
            if (order == null)
                return NotFound();

            return View(order);
        }
    }
}
