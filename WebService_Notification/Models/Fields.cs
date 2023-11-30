namespace WebService_Notification.Models
{
    public class Field
    {
        public int Id { get; set; }
        public string Keyword { get; set; }
        public string Value { get; set; }
        public string DisplayOn { get; set; }
        public int IdRequest { get; set; }
        public virtual Request Request { get; set; }

    }
}
