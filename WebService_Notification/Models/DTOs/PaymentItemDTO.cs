using System.ComponentModel.DataAnnotations.Schema;

namespace WebService_Notification.Models.DTOs
{
    public class PaymentItemDTO
    {
        [NotMapped]
        public AmountPaymentDTO Amount { get; set; }
        [NotMapped]
        public virtual StatusDTO Status { get; set; }
        public string Receipt { get; set; }
        public bool Refunded { get; set; }
        public string Franchise { get; set; }
        public string Reference { get; set; }
        public string IssuerName { get; set; }
        public string Authorization { get; set; }
        public string PaymentMethod { get; set; }
        [NotMapped]
        public virtual List<FieldDTO> ProcessorFields { get; set; }
        public int InternalReference { get; set; }
        public string PaymentMethodName { get; set; }
    }
}
