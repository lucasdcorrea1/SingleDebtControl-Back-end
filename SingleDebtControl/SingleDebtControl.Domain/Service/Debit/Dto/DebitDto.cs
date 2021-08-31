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

        public bool IsValid(IMessageErrorService messageError)
        {
            if (Value < 1)
                return messageError.AddWithReturn<bool>("Ops... não é possível adicionar um débito menor que 1!");

            if (string.IsNullOrEmpty(Description))
                return messageError.AddWithReturn<bool>("Ops... uma descrição é obrigatória!");

            return true;
        }
    }
}
