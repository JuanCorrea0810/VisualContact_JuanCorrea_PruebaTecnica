namespace WebService_Notification.Models
{
    public class StatusPayment 
    {
        public int Id { get; set; }
        public string status { get; set; }
        public string Reason { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public int IdNotification { get; set; }
        public virtual Notification Notification { get; set; }
        public int IdPaymentItem { get; set; }
        public virtual PaymentItem PaymentItem { get; set; }

    }
}
