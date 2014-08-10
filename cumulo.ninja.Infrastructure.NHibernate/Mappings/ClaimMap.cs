using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cumulo.ninja.Core.Domain.Model.Identity;
using FluentNHibernate.Mapping;

namespace cumulo.ninja.Infrastructure.NHibernate.Mappings
{
    public class ClaimMap : ClassMap<Claim>
    {
        public ClaimMap()
        {
            Table("IdentityClaim");
            
            Id(x => x.Id, "Id").GeneratedBy.Native();
            
            Map(x => x.ClaimType);
            Map(x => x.ClaimValue);

            References(o => o.User, "UserId").Not.Nullable();
        }
    }
}
