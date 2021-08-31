using SingleDebtControl.Domain.Service.Payment.Entities;
using Utils.Infra;

namespace SingleDebtControl.Domain.Service.Payment
{
    public interface IPaymentRepository : IBaseRepository<PaymentEntity>
    {
    }
}
