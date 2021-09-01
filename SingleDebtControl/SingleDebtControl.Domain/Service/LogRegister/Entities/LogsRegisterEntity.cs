using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SingleDebtControl.Domain.Service.LogRegister.Entities
{
    [Table("Logs")]
    public class LogsRegisterEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Debit")]
        public int? Id_Debit { get; set; }

        [ForeignKey("Payment")]
        public int? Id_Payment { get; set; }
    }
}