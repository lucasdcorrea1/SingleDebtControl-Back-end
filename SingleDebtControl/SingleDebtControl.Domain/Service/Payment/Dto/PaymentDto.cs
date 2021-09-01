using System;
using Utils.Message;

namespace SingleDebtControl.Domain.Service.Payment.Dto
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int Id_Debit { get; set; }
        public long Value { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public bool IsValid(IMessageService messageError)
        {
            messageError.Valid(Value < 1, "Ops... é obrigatório pagar um valor maior que zero!", "warning");
            messageError.Valid(Id_Debit <= 0, "Ops... é obrigatório informar o debito a ser descontado!", "warning");
            messageError.Valid(string.IsNullOrEmpty(Description), "Ops... é obrigatório informar uma descrição!", "warning");

            return messageError.Any();
        }
    }
}
