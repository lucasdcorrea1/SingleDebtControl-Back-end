using Microsoft.EntityFrameworkCore;
using SingleDebtControl.Domain.Service.Payment.Entities;

namespace SingleDebtControl.Infra.Repositories.Payment
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
        {
        }
        public DbSet<PaymentEntity> Payment { get; set; }
    }
}
