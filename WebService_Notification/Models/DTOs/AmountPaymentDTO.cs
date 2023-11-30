using System.ComponentModel.DataAnnotations.Schema;

namespace WebService_Notification.Models.DTOs
{
    public class AmountPaymentDTO
    {
        [NotMapped]
        public virtual AmountDTO To { get; set; }
        [NotMapped]
        public virtual AmountDTO From { get; set; }
        public int Factor { get; set; }
    }
}
