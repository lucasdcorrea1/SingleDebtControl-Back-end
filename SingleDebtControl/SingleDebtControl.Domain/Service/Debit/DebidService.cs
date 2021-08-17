using AutoMapper;
using SingleDebtControl.Domain.Service.Debit.Dto;

namespace SingleDebtControl.Domain.Service.Debit
{
    public class DebidService : IDebitService
    {
        private readonly IMapper _mapper;

        public DebidService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public DebitDto Get()
        {
            throw new System.NotImplementedException();
        }

        public int Post(DebitDto dto)
        {
            throw new System.NotImplementedException();
        }

        public bool Put(DebitDto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}
