using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebService_Notification.Models;
using WebService_Notification.Models.DTOs;

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
        public virtual DbSet<StatusDTO> StatusDTO { get; set; }
        public virtual DbSet<PayerDTO> PayerDTO { get; set; }
        public virtual DbSet<RequestDTOWithId> RequestDTO { get; set; }
        public virtual DbSet<FieldDTO> FieldsDTO { get; set; }
        public virtual DbSet<PaymentDTO> PaymentDTO { get; set; }
        public virtual DbSet<AmountDTO> AmountDTO { get; set; }
        public virtual DbSet<PaymentItemDTOWithID> PaymentItemDTO { get; set; }
        public virtual DbSet<AmountPaymentDTOWithIds> AmountPaymentDTO { get; set; }
        public virtual DbSet<NotificationDTO> NotificationDTO { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StatusDTO>().HasNoKey();
            modelBuilder.Entity<PayerDTO>().HasNoKey();
            modelBuilder.Entity<RequestDTOWithId>().HasNoKey();
            modelBuilder.Entity<FieldDTO>().HasNoKey();
            modelBuilder.Entity<PaymentDTO>().HasNoKey();
            modelBuilder.Entity<AmountDTO>().HasNoKey();
            modelBuilder.Entity<PaymentItemDTOWithID>().HasNoKey();
            modelBuilder.Entity<AmountPaymentDTOWithIds>().HasNoKey();
            modelBuilder.Entity<NotificationDTO>().HasNoKey();


            base.OnModelCreating(modelBuilder);
        }

    }
}
