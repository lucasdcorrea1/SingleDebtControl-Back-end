using AutoMapper;
using SingleDebtControl.Domain.Service.Debit.Dto;
using SingleDebtControl.Domain.Service.Debit.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

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

        public async Task<bool> Fees()
        {
            var dateNow = DateTime.Now;
            var dateCurrentMonth = new DateTime(dateNow.Year, dateNow.Month, 1);

            var LastDayMonth = dateCurrentMonth.AddMonths(1).AddDays(-1).Day;
            if (dateNow.Day != LastDayMonth)
                return false;

            using (var transaction = new TransactionScope())
            {
                var debitEntity = _debitRepository.Get(x => x.Active == true).FirstOrDefault();
                if (debitEntity == null)
                    return false;

                if (debitEntity.CreationDate.Day == dateNow.Day)
                    return false;

                debitEntity.LastUpdateDate = DateTime.Now;
                debitEntity.Active = false;
                _debitRepository.Put(debitEntity);

                var value = debitEntity.Value;
                var percent = 1.0 / 100.0;

                var debitDto = new DebitDto
                {
                    Value = ((long)(value + (percent * value))),
                    Active = true,
                    Description = debitEntity.Description,
                    CreationDate = DateTime.Now,
                };

                _debitRepository.Post(_mapper.Map<DebitEntity>(debitDto));

                transaction.Complete();
                return true;
            }
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
