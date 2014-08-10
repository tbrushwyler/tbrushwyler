using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cumulo.ninja.Core.Domain.Model.Identity;
using FluentNHibernate.Mapping;

namespace cumulo.ninja.Infrastructure.NHibernate.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("IdentityUser");

            Id(x => x.Id).Not.Nullable().GeneratedBy.Custom<StringIdGenerator>();

            Map(x => x.UserName).Unique().Not.Nullable();
            Map(x => x.Email).Unique().Not.Nullable();
            Map(x => x.PasswordHash).Nullable();
            Map(x => x.SecurityStamp).Nullable();
            Map(x => x.IsConfirmedByUser).Not.Nullable();
            Map(x => x.ConfirmationToken).Nullable();

            HasManyToMany(x => x.Roles)
                .Table("IdentityUserRole")
                .ParentKeyColumn("UserId")
                .ChildKeyColumn("RoleId")
                .Cascade.All();
            HasMany(x => x.Claims).KeyColumn("UserId").Cascade.AllDeleteOrphan();
            HasMany(x => x.Logins).KeyColumn("UserId").Cascade.AllDeleteOrphan();
        }
    }
}
