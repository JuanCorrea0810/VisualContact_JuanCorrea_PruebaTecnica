namespace WebService_Notification.Models.DTOs
{
    public class AmountPaymentDTOWithIds: AmountPaymentDTO
    {
        public int IdTo { get; set; }
        public int IdFrom { get; set; }

    }
}
