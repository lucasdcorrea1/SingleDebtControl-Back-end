using System;

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
    }
}
