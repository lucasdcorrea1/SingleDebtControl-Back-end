using AutoMapper;
using SingleDebtControl.Domain.Service.Debit;
using SingleDebtControl.Domain.Service.Debit.Dto;
using SingleDebtControl.Domain.Service.Debit.Entities;
using SingleDebtControl.Domain.Service.Payment.Dto;
using SingleDebtControl.Domain.Service.Payment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils.Message;

namespace SingleDebtControl.Domain.Service.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IDebitRepository _debitRepository;
        private readonly IMessageErrorService _messageError;

        public PaymentService(IMapper mapper, IPaymentRepository paymentRepository, IDebitRepository debitRepository, IMessageErrorService messageError)
        {
            _mapper = mapper;
            _paymentRepository = paymentRepository;
            _debitRepository = debitRepository;
            _messageError = messageError;
        }

        public IEnumerable<PaymentDto> Get()
        {
            return _mapper.Map<IEnumerable<PaymentDto>>(_paymentRepository.Get());
        }

        public int Post(PaymentDto dto)
        {
            if (dto == null)
                return _messageError.AddWithReturn<int>("Ops... é obrigatório informar os dados do pagamento!");

            if (dto.IsValid(_messageError))
                return default;

            var debitEntity = _debitRepository.Get(x => x.Id == dto.Id_Debit).FirstOrDefault();
            if (debitEntity == null)
                return _messageError.AddWithReturn<int>("Ops... não encontramos o debito para realizar o pagamento!");

            if (!debitEntity.Active)
                return _messageError.AddWithReturn<int>("Ops... debito informado já foi pago informe um debito ativo!");

            var dateNow = DateTime.Now;
            var dateCurrentMonth = new DateTime(dateNow.Year, dateNow.Month, 1);

            var LastDayMonth = dateCurrentMonth.AddMonths(1).AddDays(-1).Day;
            if (debitEntity.CreationDate.Day == LastDayMonth)
                return _messageError.AddWithReturn<int>("Ops... não é possível realizar o pagamento no ultimo dia do mês!");

            var debitValue = debitEntity.Value - dto.Value;
            if (debitValue < 0)
                return _messageError.AddWithReturn<int>("Ops... não é possível realizar um pagamento maior que a divida!");

            debitEntity.LastUpdateDate = DateTime.Now;
            debitEntity.Active = false;
            _debitRepository.Put(debitEntity);

            var debitDto = new DebitDto
            {
                Value = debitValue,
                Active = true,
                Description = debitEntity.Description,
                CreationDate = DateTime.Now,
            };

            _debitRepository.Post(_mapper.Map<DebitEntity>(debitDto));

            dto.CreationDate = DateTime.Now;
            return _paymentRepository.Post(_mapper.Map<PaymentEntity>(dto));
        }

        public bool Put(PaymentDto dto)
        {
            if (dto == null)
                return _messageError.AddWithReturn<bool>("Ops... é obrigatório informar os dados para realizar update!");

            if (dto.IsValid(_messageError))
                return default;

            if (dto.Id <= 0)
                return _messageError.AddWithReturn<bool>("Ops... é obrigatório informar o pagamento para realizar o update!");

            var debitEntity = _paymentRepository.Get(x => x.Id == dto.Id);
            if (debitEntity == null)
                return _messageError.AddWithReturn<bool>("Ops... não é possível realizar o pagamento no ultimo dia do mês!");

            _paymentRepository.Put(_mapper.Map<PaymentEntity>(dto));

            return true;
        }

    }
}
