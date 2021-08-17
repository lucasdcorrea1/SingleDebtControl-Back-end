using System.ComponentModel.DataAnnotations.Schema;

namespace SingleDebtControl.Domain.Service.Debit.Entities
{
    [Table("Debit")]
    public class DebitEntity
    {
        public int Id { get; set; }
        public long Value { get; set; }
        public string Title { get; set; }
    }
}
