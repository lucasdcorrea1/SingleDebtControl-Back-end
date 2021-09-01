using System;
using Utils.Message;

namespace SingleDebtControl.Domain.Service.Debit.Dto
{
    public class DebitDto
    {
        public int Id { get; set; }
        public long Value { get; set; }
        public bool Active { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public bool IsValid(INotification messageError)
        {
            messageError.Valid(Value < 1, "Ops... não é possível adicionar um débito menor que zero!", "warning");
            messageError.Valid(string.IsNullOrEmpty(Description), "Ops... uma descrição é obrigatória!", "warning");

            return messageError.Any();
        }
    }
}
