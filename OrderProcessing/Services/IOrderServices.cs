using OrderProcessing.ViewModels;

namespace OrderProcessing.Services
{
    public interface IOrderServices
    {
        Task<CheckoutViewModel> GetCheckoutDataAsync();
        Task<int> PlaceOrderAsync(CheckoutViewModel model);

    }
}
