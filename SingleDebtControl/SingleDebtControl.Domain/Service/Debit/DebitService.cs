using AutoMapper;
using SingleDebtControl.Domain.Service.Debit.Dto;
using System.Collections.Generic;

namespace SingleDebtControl.Domain.Service.Debit
{
    public class DebitService : IDebitService
    {
        private readonly IMapper _mapper;
        private readonly IDebitRepository _debitRepository;

        public DebitService(IMapper mapper, IDebitRepository debitRepository) 
        {
            _mapper = mapper;
            _debitRepository = debitRepository;
        }

        public  IEnumerable<DebitDto> Get()
        {
            return _mapper.Map<IEnumerable<DebitDto>>(_debitRepository.Get());
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
