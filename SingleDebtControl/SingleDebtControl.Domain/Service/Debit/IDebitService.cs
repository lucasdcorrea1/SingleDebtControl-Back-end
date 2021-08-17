using SingleDebtControl.Domain.Service.Debit.Dto;

namespace SingleDebtControl.Domain.Service.Debit
{
    public interface IDebitService
    {
        DebitDto Get();
        int Post(DebitDto dto);
        bool Put(DebitDto dto);
        
    }
}
