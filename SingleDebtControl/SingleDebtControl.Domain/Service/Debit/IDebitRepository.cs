using SingleDebtControl.Domain.Base;
using SingleDebtControl.Domain.Service.Debit.Entities;

namespace SingleDebtControl.Domain.Service.Debit
{
    public interface IDebitRepository : IBaseRepository<DebitEntity>
    {
    }
}
