using Microsoft.EntityFrameworkCore;
using SingleDebtControl.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Utils.Infra
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        protected BaseRepository(DbContext context)
        {
            Context = context;
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

        public IEnumerable<TEntity> Get()
        {
            return Context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            return Context.Set<TEntity>().Where(filter).ToList();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public void Delete(TEntity item)
        {
            Context.Set<TEntity>().Remove(item);
            Context.SaveChanges();
        }
    }
}
