using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SingleDebtControl.Domain.Service.Debit;

namespace SingleDebtControl.Api.Hangfire
{
    public static class HangFireTasks
    {
        public static void StartHangFireTasks(this ServiceProvider ServiceProvider, IApplicationBuilder app)
        {
            app.UseHangfireDashboard();

            var debitService = ServiceProvider.GetService<IDebitService>();
            RecurringJob.AddOrUpdate(() => debitService.AddTax(), Cron.DayInterval(1));
        }
    }
}
