using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SingleDebtControl.Domain.Service.Debit;
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


            services.ResolveContexts(x => x.UseSqlServer("",
             providerOptions => providerOptions.CommandTimeout(20)));

            Repositories(services);
            Services(services);
        }

        private static void ResolveContexts(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction = null)
        {


        }

        public static void Repositories(IServiceCollection services)
        {
            //services.AddScoped<IDebitRepository, DebitRepository>();
        }

        public static void Services(IServiceCollection services)
        {
            services.AddScoped<IDebitService, DebitService>();
        }
    }
}
