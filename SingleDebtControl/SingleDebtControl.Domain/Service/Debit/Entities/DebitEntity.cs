using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SingleDebtControl.Domain.Service.Debit.Entities
{
    [Table("Debits")]
    public class DebitEntity
    {
        public int Id { get; set; }
        public long Value { get; set; }
        public bool Active { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}
