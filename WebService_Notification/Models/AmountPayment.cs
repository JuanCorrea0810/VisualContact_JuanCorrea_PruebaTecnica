namespace WebService_Notification.Models
{
    public class AmountPayment
    {
        public int Id { get; set; }
        public int Factor { get; set; }
        public int IdTo { get; set; }
        public int IdFrom { get; set; }
        public virtual Amount To { get; set; }
        public virtual Amount From { get; set; }

    }
}
