using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SingleDebtControl.Domain.Base
{
    public interface IBaseRepository <T> where T : class
    {
        IEnumerable<T> Get();
        int Post(T item);
        void Delete(T item);
        void Put(T item);
    }
}
