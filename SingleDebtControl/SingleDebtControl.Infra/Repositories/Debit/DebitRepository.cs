using SingleDebtControl.Domain.Service.Debit;
using SingleDebtControl.Domain.Service.Debit.Entities;
using SingleDebtControl.Infra.Base;
using SingleDebtControl.Infra.Context;

namespace SingleDebtControl.Infra.Repositories.Debit
{
    public class DebitRepository : BaseRepository<DebitEntity>, IDebitRepository
    {
        public DebitRepository(DebitContext context) : base(context)
        {
        }
    }
}
