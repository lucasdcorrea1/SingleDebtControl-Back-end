using Microsoft.EntityFrameworkCore;
using SingleDebtControl.Domain.Base;
using SingleDebtControl.Infra.Context;
using SingleDebtControl.Infra.Repositories.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SingleDebtControl.Infra.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly DebitContext Context;

        protected BaseRepository(DebitContext context)
        {
            Context = context;
        }

        public BaseRepository(PaymentContext context)
        {
        }

        public IEnumerable<TEntity> Get()
        {
            return Context.Set<TEntity>().ToList();
        }
        public int Post(TEntity item)
        {
            Context.Set<TEntity>().Add(item);
            Context.SaveChanges();

            var idProperty = item.GetType().GetProperty("Id")?.GetValue(item, null);

            if (!(Convert.ChangeType(idProperty, typeof(int)) is int intTried))
                return 0;

            return (int)intTried;
        }
        public void Put(TEntity item)
        {
            Context.Set<TEntity>().Attach(item);
            Context.Entry(item).State = EntityState.Modified;
            Context.SaveChanges();
        }
        public void Delete(TEntity item)
        {
            Context.Set<TEntity>().Remove(item);
            Context.SaveChanges();
        }
    }
}
