using cumulo.ninja.Infrastructure.NHibernate.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace cumulo.ninja.Infrastructure.NHibernate
{
    public class NHibernateSessionFactory
    {
        public ISessionFactory GetSessionFactory()
        {
            ISessionFactory fluentConfiguration = Fluently.Configure()
                                                   .Database(MsSqlConfiguration.MsSql2012.ConnectionString(c => c.FromConnectionStringWithKey("Default")))
                                                   .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>())
                                                   .BuildSessionFactory();

            return fluentConfiguration;
        }
    }
}
