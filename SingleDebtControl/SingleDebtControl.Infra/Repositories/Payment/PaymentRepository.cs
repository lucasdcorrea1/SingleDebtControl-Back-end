using SingleDebtControl.Domain.Service.Payment;
using SingleDebtControl.Domain.Service.Payment.Entities;
using SingleDebtControl.Infra.Base;

namespace SingleDebtControl.Infra.Repositories.Payment
{
    public class PaymentRepository : BaseRepository<PaymentEntity>, IPaymentRepository
    {
        public PaymentRepository(PaymentContext context) : base(context)
        {
        }
    }
}
