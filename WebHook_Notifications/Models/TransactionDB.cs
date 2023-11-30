namespace WebHook_Notifications.Models
{
    public class TransactionDB
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public string PaymentMethod { get; set; }
        public string IssuerName { get; set; }
        public double Value { get; set; }
        public virtual NotificationDB Notification { get; set; }
        public virtual PayerDB Payer { get; set; }
    }
}
