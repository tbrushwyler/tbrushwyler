using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cumulo.ninja.Core.Domain.Model.Identity;
using FluentNHibernate.Mapping;

namespace cumulo.ninja.Infrastructure.NHibernate.Mappings
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Table("IdentityRole");

            Id(x => x.Id).Not.Nullable().GeneratedBy.Custom<StringIdGenerator>();

            Map(x => x.Name).Unique().Not.Nullable();

            HasManyToMany(x => x.Users)
                .Table("IdentityUserRole")
                .Inverse()
                .Cascade.All();
        }
    }
}
