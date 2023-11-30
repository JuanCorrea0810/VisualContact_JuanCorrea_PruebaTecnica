using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebHook_Notifications.Models;

namespace WebHook_Notifications
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

        public virtual DbSet<NotificationDB> Notification { get; set; }
        public virtual DbSet<TransactionDB> Transaction { get; set; }
        public virtual DbSet<PayerDB> Payer { get; set; }


    }
}
