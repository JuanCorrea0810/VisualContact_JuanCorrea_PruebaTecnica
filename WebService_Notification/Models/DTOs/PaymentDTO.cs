using System.ComponentModel.DataAnnotations.Schema;

namespace WebService_Notification.Models.DTOs
{
    public class PaymentDTO
    {
        public string Reference { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public virtual AmountDTO Amount { get; set; }
        public bool AllowPartial { get; set; }
        public bool Subscribe { get; set; }
    }
}
