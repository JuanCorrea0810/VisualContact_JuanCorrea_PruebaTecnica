using System.ComponentModel.DataAnnotations.Schema;

namespace WebHook_Notifications.Models.DTOs
{
    public class AmountPaymentDTO
    {
        public  AmountDTO To { get; set; }
        public  AmountDTO From { get; set; }
        public int Factor { get; set; }
    }
}
