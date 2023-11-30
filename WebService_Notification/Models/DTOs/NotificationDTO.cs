using System.ComponentModel.DataAnnotations.Schema;

namespace WebService_Notification.Models.DTOs
{
    public class NotificationDTO
    {
        public int RequestId { get; set; }
        [NotMapped]
        public virtual StatusDTO Status { get; set; }
        [NotMapped]
        public virtual RequestDTO Request { get; set; }
        [NotMapped]
        public virtual List<PaymentItemDTO> Payment { get; set; }
        public string Subscription { get; set; }
    }
}
