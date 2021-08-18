using Microsoft.EntityFrameworkCore;
using SingleDebtControl.Domain.Service.Debit.Entities;

namespace SingleDebtControl.Infra.Context
{
    public class DebitContext : DbContext
    {
        public DebitContext(DbContextOptions<DebitContext> options) : base(options)
        {
        }
        public DbSet<DebitEntity> Debit { get; set; }
    }
}
