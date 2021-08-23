
using AutoMapper;
using SingleDebtControl.Domain.Service.Payment.Dto;
using System.Collections.Generic;

namespace SingleDebtControl.Domain.Service.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IMapper _mapper;

        public PaymentService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<PaymentDto> Get()
        {
            throw new System.NotImplementedException();
        }

        public int Post(PaymentDto dto)
        {
            throw new System.NotImplementedException();
        }

        public bool Put(PaymentDto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}
