using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cumulo.ninja.Core.Domain.Model.Identity;
using FluentNHibernate.Mapping;

namespace cumulo.ninja.Infrastructure.NHibernate.Mappings
{
    public class LoginMap : ClassMap<Login>
    {
        public LoginMap()
        {
            Table("IdentityLogin");

            Id(x => x.Id).GeneratedBy.Native();

            Map(x => x.LoginProvider);
            Map(x => x.ProviderKey);

            References(x => x.User, "UserId").Not.Nullable();
        }
    }
}
