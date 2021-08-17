using SingleDebtControl.Domain.Service.Debit.Dto;
using SingleDebtControl.Domain.Service.Debit.Entities;

namespace SingleDebtControl.Api.Infra
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DebitEntity, DebitDto>();
            CreateMap<DebitDto, DebitEntity>();
        }
    }
}
