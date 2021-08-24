
using AutoMapper;
using SingleDebtControl.Domain.Service.Debit;
using SingleDebtControl.Domain.Service.Debit.Dto;
using SingleDebtControl.Domain.Service.Debit.Entities;
using SingleDebtControl.Domain.Service.Payment.Dto;
using SingleDebtControl.Domain.Service.Payment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace SingleDebtControl.Domain.Service.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IDebitRepository _debitRepository;

        public PaymentService(IMapper mapper, IPaymentRepository paymentRepository, IDebitRepository debitRepository)
        {
            _mapper = mapper;
            _paymentRepository = paymentRepository;
            _debitRepository = debitRepository;
        }

        public IEnumerable<PaymentDto> Get()
        {
            return _mapper.Map<IEnumerable<PaymentDto>>(_paymentRepository.Get());
        }

        public int Post(PaymentDto dto)
        {
            using (var transaction = new TransactionScope())
            {
                var debitEntity = _debitRepository.Get(x => x.Id == dto.Id_Debit && x.Active == true).FirstOrDefault();
                if (debitEntity == null)
                    return 0;


                var dateNow = DateTime.Now;
                var dateCurrentMonth = new DateTime(dateNow.Year, dateNow.Month, 1);

                var LastDayMonth = dateCurrentMonth.AddMonths(1).AddDays(-1).Day;
                if (debitEntity.CreationDate.Day == LastDayMonth)
                    return 0;

                debitEntity.LastUpdateDate = DateTime.Now;
                debitEntity.Active = false;
                _debitRepository.Put(debitEntity);

                var debitDto = new DebitDto
                {
                    Value = debitEntity.Value - dto.Value,
                    Active = true,
                    Description = debitEntity.Description,
                    CreationDate = DateTime.Now,
                };

                _debitRepository.Post(_mapper.Map<DebitEntity>(debitDto));

                dto.CreationDate = DateTime.Now;
                var id = _paymentRepository.Post(_mapper.Map<PaymentEntity>(dto));

                transaction.Complete();

                return id;
            }
        }

        public bool Put(PaymentDto dto)
        {
            _paymentRepository.Put(_mapper.Map<PaymentEntity>(dto));

            return true;
        }

    }
}
