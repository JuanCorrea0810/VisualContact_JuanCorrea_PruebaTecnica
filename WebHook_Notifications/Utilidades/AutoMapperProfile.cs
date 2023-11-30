using AutoMapper;
using WebHook_Notifications.Models;
using WebHook_Notifications.Models.DTOs;

namespace WebHook_Notifications.Utilidades
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<NotificationDTO,NotificationDB>()
                .ForMember(x=> x.DateTime , z=>z.MapFrom(o=> DateTime.Now))
                .ForMember(x=> x.TransactionId,z=> z.MapFrom(o=> o.RequestId));

            CreateMap<NotificationDTO, TransactionDB>()
                .ForMember(x=> x.Id,z=> z.MapFrom(o=> o.RequestId))
                .ForMember(x => x.Status, z => z.MapFrom(o => o.Status.status))
                .ForMember(x => x.Message, z => z.MapFrom(o => o.Status.Message))
                .ForMember(x => x.DateTime, z => z.MapFrom(o => o.Status.Date))
                .ForMember(x => x.Value, z => z.MapFrom(o => o.Request.Payment.Amount.Total))
                .ForMember(x=> x.PayerId, z=> z.MapFrom(o=> o.Request.Payer.Document))
                .AfterMap((dto,transaction) => 
                {
                    if (dto.Payment != null)
                    {
                        foreach (var item in dto.Payment)
                        {
                            transaction.IssuerName = item.IssuerName;
                            transaction.PaymentMethod = item.PaymentMethod;
                        }
                    }
                });
            CreateMap<NotificationDTO, PayerDB>()
                .ForMember(x => x.Document, z => z.MapFrom(o => o.Request.Payer.Document))
                .ForMember(x => x.Name, z => z.MapFrom(o => o.Request.Payer.Name))
                .ForMember(x => x.Surname, z => z.MapFrom(o => o.Request.Payer.Surname))
                .ForMember(x => x.Email, z => z.MapFrom(o => o.Request.Payer.Email))
                .ForMember(x => x.Mobile, z => z.MapFrom(o => o.Request.Payer.Mobile))
                ;
        }
    }
}
