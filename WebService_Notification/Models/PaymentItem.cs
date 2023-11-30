namespace WebService_Notification.Models
{
    public class PaymentItem
    {
        public int Id { get; set; }
        public string Receipt { get; set; }
        public bool Refunded { get; set; }
        public string Franchise { get; set; }
        public string Reference { get; set; }
        public string IssuerName { get; set; }
        public string Authorization { get; set; }
        public string PaymentMethod { get; set; }
        public int InternalReference { get; set; }
        public string PaymentMethodName { get; set; }
        public int IdNotification { get; set; }
        public int IdAmount { get; set; }
        public virtual AmountPayment Amount { get; set; }
        public virtual List<StatusPayment> Status { get; set; }
        public virtual List<FieldsPayment> ProcessorFields { get; set; }
        public virtual Notification Notification { get; set; }
    }
}
