using SingleDebtControl.Domain.Service.Debit.Entities;
using Utils.Infra;

namespace SingleDebtControl.Domain.Service.Debit
{
    public interface IDebitRepository : IBaseRepository<DebitEntity>
    {
    }
}
