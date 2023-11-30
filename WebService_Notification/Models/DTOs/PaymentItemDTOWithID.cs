namespace WebService_Notification.Models.DTOs
{
    public class PaymentItemDTOWithID:PaymentItemDTO
    {
        public int Id { get; set; }
        public int IdAmount { get; set; }
    }
}
