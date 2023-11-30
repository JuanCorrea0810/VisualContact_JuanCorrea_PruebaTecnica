namespace WebService_Notification.Models
{
    public class Amount
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public double Total { get; set; }
        public string IdPayment { get; set; }
        public virtual Payment Payment { get; set; }

    }
}
