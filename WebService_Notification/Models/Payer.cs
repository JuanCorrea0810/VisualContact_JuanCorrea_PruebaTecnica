using System.ComponentModel.DataAnnotations;

namespace WebService_Notification.Models
{
    public class Payer
    {
        [Key]
        public string Document { get; set; }
        public string DocumentType { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

        public virtual List<Request> Requests { get; set; }
    }
}
