using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cumulo.ninja.Infrastructure.NHibernate;
using NHibernate;
using Ninject.Activation;

namespace cumulo.ninja.Infrastructure.IoC.NHibernate
{
    public class NhibernateSessionFactoryProvider : Provider<ISessionFactory>
    {
        protected override ISessionFactory CreateInstance(IContext context)
        {
            var sessionFactory = new NHibernateSessionFactory();
            return sessionFactory.GetSessionFactory();
        }
    }
}
