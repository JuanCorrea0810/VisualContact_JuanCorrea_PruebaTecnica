using System.ComponentModel.DataAnnotations;

namespace WebService_Notification.Models
{
    public class Payment
    {
        [Key]
        public string Reference { get; set; }
        public string Description { get; set; }
        public bool AllowPartial { get; set; }
        public bool Subscribe { get; set; }
        public int IdRequest { get; set; }
        public virtual Request Request { get; set; }
        public virtual List<Amount> Amount { get; set; }

    }
}
