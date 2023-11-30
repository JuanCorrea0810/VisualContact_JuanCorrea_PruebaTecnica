namespace WebHook_Notifications.Models
{
    public class NotificationDB
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int TransactionId { get; set; }
        public virtual TransactionDB Transaction { get; set; }

    }
}
