using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace cumulo.ninja.Core.Domain.Model
{
    public interface IRepository<T> where T : IEntity<T>
    {
        T Get(object id);

        T Load(object id);

        IEnumerable<T> GetAll();

        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        T FindOneBy(Expression<Func<T, bool>> predicate);

        IQueryable<T> Query();

        void Store(T entity);

        void Delete(T entity);
    }
}
