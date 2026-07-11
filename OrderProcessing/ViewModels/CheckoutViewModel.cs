using System.ComponentModel.DataAnnotations;

namespace OrderProcessing.ViewModels
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Please Enter Your Name ")]
        public String CustomerName { get ; set; } = "";
        public List<CheckoutItemViewModel> Items { get; set; } = new();
    }
}
