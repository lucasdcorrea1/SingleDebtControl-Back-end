
using AutoMapper;
using SingleDebtControl.Domain.Service.Payment.Dto;
using SingleDebtControl.Domain.Service.Payment.Entities;
using System;
using System.Collections.Generic;

namespace SingleDebtControl.Domain.Service.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IMapper mapper, IPaymentRepository paymentRepository)
        {
            _mapper = mapper;
            _paymentRepository = paymentRepository;
        }

        public IEnumerable<PaymentDto> Get()
        {
            return _mapper.Map<IEnumerable<PaymentDto>>(_paymentRepository.Get());
        }

        public int Post(PaymentDto dto)
        {

            dto.CreationDate = DateTime.Now;
            var id = _paymentRepository.Post(_mapper.Map<PaymentEntity>(dto));
            return id;
        }

        public bool Put(PaymentDto dto)
        {

            _paymentRepository.Put(_mapper.Map<PaymentEntity>(dto));

            return true;
        }

    }
}
