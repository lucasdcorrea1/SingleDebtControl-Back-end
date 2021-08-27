using SingleDebtControl.Domain.Service.Debit.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SingleDebtControl.Domain.Service.Debit
{
    public interface IDebitService
    {
        IEnumerable<DebitDto> Get();
        IEnumerable<DebitDto> Get(bool ative);
        int Post(DebitDto dto);
        bool Put(DebitDto dto);

        Task<bool> AddTax();
    }
}
