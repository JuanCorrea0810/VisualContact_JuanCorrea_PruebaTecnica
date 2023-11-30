namespace WebService_Notification.Models
{
    public class FieldsPayment
    {
        public int Id { get; set; }
        public string Keyword { get; set; }
        public string Value { get; set; }
        public string DisplayOn { get; set; }
        public int IdPaymentItem { get; set; }
        public virtual PaymentItem PaymentItem { get; set; }
    }
}
