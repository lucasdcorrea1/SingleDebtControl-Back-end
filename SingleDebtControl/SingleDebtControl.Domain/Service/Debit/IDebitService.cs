using SingleDebtControl.Domain.Service.Debit.Dto;
using System.Collections.Generic;

namespace SingleDebtControl.Domain.Service.Debit
{
    public interface IDebitService
    {
        IEnumerable<DebitDto> Get();
        int Post(DebitDto dto);
        bool Put(DebitDto dto);
        
    }
}
