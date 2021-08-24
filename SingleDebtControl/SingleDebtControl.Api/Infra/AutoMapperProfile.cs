using SingleDebtControl.Domain.Service.Debit.Dto;
using SingleDebtControl.Domain.Service.Debit.Entities;
using SingleDebtControl.Domain.Service.Payment.Dto;
using SingleDebtControl.Domain.Service.Payment.Entities;

namespace SingleDebtControl.Api.Infra
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DebitEntity, DebitDto>();
            CreateMap<DebitDto, DebitEntity>();

            CreateMap<PaymentEntity, PaymentDto>();
            CreateMap<PaymentDto, PaymentEntity>();
        }
    }
}
