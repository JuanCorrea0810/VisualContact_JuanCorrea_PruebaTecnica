using System.ComponentModel.DataAnnotations;

namespace WebHook_Notifications.Models
{
    public class PayerDB
    {
        [Key]
        public string Document { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int TransactionId { get; set; }
        public virtual TransactionDB Transaction { get; set; }

    }
}
