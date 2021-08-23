using SingleDebtControl.Domain.Service.Payment.Dto;
using System.Collections.Generic;

namespace SingleDebtControl.Domain.Service.Payment
{
    public interface IPaymentService
    {
        int Post(PaymentDto dto);
        bool Put(PaymentDto dto);
        IEnumerable<PaymentDto> Get();
    }
}
