using System.ComponentModel.DataAnnotations.Schema;

namespace WebService_Notification.Models.DTOs
{
    public class RequestDTO
    {
        public string Locale { get; set; }
        [NotMapped]
        public virtual PayerDTO Payer { get; set; }
        [NotMapped]
        public virtual PaymentDTO Payment { get; set; }
        [NotMapped]
        public virtual List<FieldDTO> Fields { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTime Expiration { get; set; }
    }
}
