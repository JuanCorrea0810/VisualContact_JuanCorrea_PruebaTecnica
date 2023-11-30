using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebService_Notification.Models
{
    public class Notification
    {
        [Key]
        public int RequestId { get; set; }
        public string Subscription { get; set; }
        [NotMapped]
        public virtual List<Status> Status { get; set; }
        [NotMapped]
        public virtual List<Request> Request { get; set; }
        [NotMapped]
        public virtual List<PaymentItem> Payment { get; set; }


    }
}
