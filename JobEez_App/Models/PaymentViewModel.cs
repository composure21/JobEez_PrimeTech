namespace JobEez_App.Models
{
    public class PaymentViewModel
    {
        public string MerchantId { get; set; }
        public string MerchantKey { get; set; }
      
        public string Amount { get; set; }
        public string ItemName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string ReturnUrl { get; set; }
        public string CancelUrl { get; set; }
        public string NotifyUrl { get; set; }
    }
}
