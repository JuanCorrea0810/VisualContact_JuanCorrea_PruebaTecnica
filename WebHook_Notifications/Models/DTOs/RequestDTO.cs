using System.ComponentModel.DataAnnotations.Schema;

namespace WebHook_Notifications.Models.DTOs
{
    public class RequestDTO
    {
        public string Locale { get; set; }
        public  PayerDTO Payer { get; set; }
        public  PaymentDTO Payment { get; set; }
        public  List<FieldDTO> Fields { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTime Expiration { get; set; }
    }
}
