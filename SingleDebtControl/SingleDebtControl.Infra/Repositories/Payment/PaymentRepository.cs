using SingleDebtControl.Domain.Service.Payment;
using SingleDebtControl.Domain.Service.Payment.Entities;
using Utils.Infra;

namespace SingleDebtControl.Infra.Repositories.Payment
{
    public class PaymentRepository : BaseRepository<PaymentEntity>, IPaymentRepository
    {
        public PaymentRepository(PaymentContext context) : base(context)
        {
        }
    }
}
