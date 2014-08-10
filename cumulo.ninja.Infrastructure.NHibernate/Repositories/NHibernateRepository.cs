using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using cumulo.ninja.Core.Domain;
using cumulo.ninja.Core.Domain.Model;
using NHibernate;
using NHibernate.Linq;

namespace cumulo.ninja.Infrastructure.NHibernate.Repositories
{
    public class NHibernateRepository<T> : IRepository<T> where T : class, IEntity<T>
    {
        protected ISession Session;

        public NHibernateRepository(ISession session)
        {
            Session = session;
        }

        public virtual T Get(object id)
        {
            return Session.Get<T>(id);
        }

        public virtual T Load(object id)
        {
            return Session.Load<T>(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Query().AsEnumerable();
        }

        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> condition)
        {
            return Query().Where(condition).ToList<T>();
        }

        public virtual T FindOneBy(Expression<Func<T, bool>> condition)
        {
            return Query().Where(condition).FirstOrDefault();
        }

        public virtual IQueryable<T> Query()
        {
            return Session.Query<T>();
        }

        public virtual IQueryOver<T> QueryOver(Expression<Func<T, bool>> condition)
        {
            return Session.QueryOver<T>().Where(condition);
        }

        public virtual void Store(T entity)
        {
            using (ITransaction transaction = Session.BeginTransaction())
            {
                Session.SaveOrUpdate(entity);
                transaction.Commit();
            }
        }

        public virtual void Delete(T entity)
        {
            using (ITransaction transaction = Session.BeginTransaction())
            {
                Session.Delete(entity);
                transaction.Commit();
            }
        }
    }
}
