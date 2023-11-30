using System.ComponentModel.DataAnnotations.Schema;

namespace WebHook_Notifications.Models.DTOs
{
    public class PaymentDTO
    {
        public string Reference { get; set; }
        public string Description { get; set; }
        public  AmountDTO Amount { get; set; }
        public bool AllowPartial { get; set; }
        public bool Subscribe { get; set; }
    }
}
