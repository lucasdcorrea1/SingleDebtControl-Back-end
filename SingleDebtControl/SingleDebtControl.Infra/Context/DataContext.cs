using Microsoft.EntityFrameworkCore;
using SingleDebtControl.Domain.Service.Debit.Entities;

namespace SingleDebtControl.Infra.Context
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DbSet<DebitEntity> Debit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseSqlServer("DebitControl");
        }
    }
}
