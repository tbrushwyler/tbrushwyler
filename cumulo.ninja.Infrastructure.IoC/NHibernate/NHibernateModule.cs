using cumulo.ninja.Core.Domain.Model;
using cumulo.ninja.Infrastructure.NHibernate.Repositories;
using NHibernate;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;

namespace cumulo.ninja.Infrastructure.IoC.NHibernate
{
    public class NHibernateModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IRepository<>)).To(typeof(NHibernateRepository<>));
            Bind<ISessionFactory>().ToProvider<NhibernateSessionFactoryProvider>().InSingletonScope();
            Bind<ISession>().ToMethod(context => context.Kernel.Get<ISessionFactory>().OpenSession()).InRequestScope();
        }
    }
}
