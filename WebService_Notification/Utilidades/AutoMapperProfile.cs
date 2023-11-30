using AutoMapper;
using WebService_Notification.Models.DTOs;

namespace WebService_Notification.Utilidades
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RequestDTOWithId, RequestDTO>();
            CreateMap<AmountPaymentDTOWithIds,AmountPaymentDTO>();
            CreateMap<PaymentItemDTOWithID, PaymentItemDTO>();
        }
    }
}
