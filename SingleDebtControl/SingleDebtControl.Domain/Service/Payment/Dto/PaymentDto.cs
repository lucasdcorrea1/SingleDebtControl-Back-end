using System;

namespace SingleDebtControl.Domain.Service.Payment.Dto
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int Id_Debit { get; set; }
        public long Value { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
