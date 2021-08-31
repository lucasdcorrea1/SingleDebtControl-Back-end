using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Utils.Infra
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> Get();
        IEnumerable<T> Get(Expression<Func<T, bool>> filter);
        int Post(T item);
        void Delete(T item);
        void Put(T item);
    }
}
