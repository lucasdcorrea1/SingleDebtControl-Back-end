using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SingleDebtControl.Domain.Service.Debit;
using SingleDebtControl.Infra.Context;
using SingleDebtControl.Infra.Repositories.Debit;
using System;

namespace SingleDebtControl.Api.Infra
{
    public static class DependencyResolver
    {
        public static void Resolve(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new AutoMapperProfile());
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.ResolveContexts(x => x.UseSqlServer("data source=DESKTOP-NK2FLPE\\SQLUCAS;initial catalog=DebitControl;password=cloudmed; user id=sa",
         providerOptions => providerOptions.CommandTimeout(20)));

            Repositories(services);
            Services(services);
        }


        private static void ResolveContexts(this IServiceCollection services,
            Action<DbContextOptionsBuilder> optionsAction = null)
        {
            services.AddDbContext<DebitContext>(optionsAction);
        }
        public static void Repositories(IServiceCollection services)
        {
            services.AddScoped<IDebitRepository, DebitRepository>();
        }

        public static void Services(IServiceCollection services)
        {
            services.AddScoped<IDebitService, DebitService>();
        }
    }
}
