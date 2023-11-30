using Microsoft.EntityFrameworkCore;
using WebService_Notification.Models;

namespace WebService_Notification
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<Amount> Amount { get; set; }
        public virtual DbSet<AmountPayment> AmountPayment { get; set; }
        public virtual DbSet<Field> Field { get; set; }
        public virtual DbSet<Payer> Payer { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<PaymentItem> PaymentItem { get; set; }
        public virtual DbSet<Request> Request { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<StatusPayment> StatusPayment { get; set; }
        public virtual DbSet<FieldsPayment> FieldsPayment { get; set; }
    }
}
