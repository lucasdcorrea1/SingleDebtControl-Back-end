using SingleDebtControl.Domain.Base;
using SingleDebtControl.Domain.Service.Payment.Entities;

namespace SingleDebtControl.Domain.Service.Payment
{
    public interface IPaymentRepository: IBaseRepository<PaymentEntity>
    {
    }
}
