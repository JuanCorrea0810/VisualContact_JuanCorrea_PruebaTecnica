namespace WebHook_Notifications.Models.DTOs
{
    public class NotificationDTO
    {
        public int RequestId { get; set; }
        
        public  StatusDTO Status { get; set; }
        
        public  RequestDTO Request { get; set; }
        
        public  List<PaymentItemDTO> Payment { get; set; }
        public string Subscription { get; set; }
    }
}
