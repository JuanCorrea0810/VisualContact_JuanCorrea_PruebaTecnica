namespace WebService_Notification.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string Locale { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTime Expiration { get; set; }
        public int IdNotification { get; set; }
        public string IdPayer { get; set; }
        public virtual Payer Payer { get; set; }
        public virtual List<Payment> Payment { get; set; }
        public virtual Notification Notification { get; set; }
        public virtual List<Field> Fields { get; set; }

    }
}
