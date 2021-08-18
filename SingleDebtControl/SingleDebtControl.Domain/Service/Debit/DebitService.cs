using AutoMapper;
using SingleDebtControl.Domain.Service.Debit.Dto;
using SingleDebtControl.Domain.Service.Debit.Entities;
using System;
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

        public IEnumerable<DebitDto> Get()
        {
            return _mapper.Map<IEnumerable<DebitDto>>(_debitRepository.Get());
        }

        public int Post(DebitDto dto)
        {

            dto.LastUpdateDate = DateTime.Now;
            dto.CreationDate = DateTime.Now;
            var id = _debitRepository.Post(_mapper.Map<DebitEntity>(dto));
            return id;
        }

        public bool Put(DebitDto dto)
        {
            dto.LastUpdateDate = DateTime.Now;

            _debitRepository.Put(_mapper.Map<DebitEntity>(dto));

            return true;
        }
    }
}
