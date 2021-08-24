using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SingleDebtControl.Domain.Service.Payment.Entities
{
 
    [Table("Payment")]
    public class PaymentEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Debit")]
        public int Id_Debit { get; set; }

        public long Value { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
