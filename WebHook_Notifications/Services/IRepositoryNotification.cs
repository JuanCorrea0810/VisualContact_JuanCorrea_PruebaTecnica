using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebHook_Notifications.Models;
using WebHook_Notifications.Models.DTOs;

namespace WebHook_Notifications.Services
{
    public interface IRepositoryNotification
    {
        Task<bool> ExistsId(int RequestId);
        Task InsertData(NotificationDTO data);
    }

    public class RepositoryNotification : IRepositoryNotification
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RepositoryNotification(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task InsertData(NotificationDTO data) 
        {
            var Notification = mapper.Map<NotificationDB>(data);
            var Transaction = mapper.Map<TransactionDB>(data);
            var Payer = mapper.Map<PayerDB>(data);
            
            var existsPayer = await context.Payer.AnyAsync(x=> x.Document == Payer.Document);
            
            if (!existsPayer)
            {
                context.Add(Payer);
            }
            context.Add(Transaction);
            context.Add(Notification);

            await context.SaveChangesAsync();
        }

        public async Task<bool> ExistsId(int RequestId) 
        {
            var result = await context.Transaction.AnyAsync(x=> x.Id == RequestId);
            return result;
        }



    }
}
