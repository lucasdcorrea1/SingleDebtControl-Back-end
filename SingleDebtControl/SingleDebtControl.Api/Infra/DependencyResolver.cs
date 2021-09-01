using AutoMapper;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SingleDebtControl.Domain.Service.Debit;
using SingleDebtControl.Domain.Service.Payment;
using SingleDebtControl.Infra.Context;
using SingleDebtControl.Infra.Repositories.Debit;
using SingleDebtControl.Infra.Repositories.Payment;
using System;
using Utils.Environments;
using Utils.Message;

namespace SingleDebtControl.Api.Infra
{
    public static class DependencyResolver
    {
        public static void Resolve(this IServiceCollection services)
        {
            services.AddScoped<Parameters>();

            var parameters = new Parameters();

            var mappingConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new AutoMapperProfile());
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.ResolveContexts(x => x.UseSqlServer(parameters.Data.ConnectionString,
                                     providerOptions => providerOptions.CommandTimeout(20)));

            #region Hangfire
            services.AddHangfire(x => x.UseSqlServerStorage(parameters.Data.ConnectionStringHangfire));
            services.AddHangfireServer();
            #endregion

            Repositories(services);
            Services(services);
        }

        private static void ResolveContexts(this IServiceCollection services,
            Action<DbContextOptionsBuilder> optionsAction = null)
        {
            services.AddDbContext<DebitContext>(optionsAction);
            services.AddDbContext<PaymentContext>(optionsAction);
        }
        public static void Repositories(IServiceCollection services)
        {
            services.AddScoped<IDebitRepository, DebitRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
        }

        public static void Services(IServiceCollection services)
        {
            services.AddScoped<IDebitService, DebitService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<INotification, Notification>();
        }
    }
}
