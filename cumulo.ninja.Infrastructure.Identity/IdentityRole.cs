using System.Collections.Generic;
using cumulo.ninja.Core.Domain.Model.Identity;
using Microsoft.AspNet.Identity;
using System.Collections.ObjectModel;

namespace cumulo.ninja.Infrastructure.Identity
{
    public class IdentityRole : IRole
    {
        public Role BaseType { get; set; }

        public IdentityRole(Role baseType)
        {
            BaseType = baseType;
        }

        public string Id
        {
            get { return BaseType.Id; }
            set { BaseType.Id = value; }
        }

        public string Name
        {
            get { return BaseType.Name; }
            set { BaseType.Name = value; }
        }

        public ICollection<IdentityUser> Users
        {
            get
            {
                ICollection<IdentityUser> users = new Collection<IdentityUser>();
                foreach (User coreUser in this.BaseType.Users)
                {
                    users.Add(new IdentityUser(coreUser));
                }
                return users;
            }
            protected set
            {
                this.Users = value;
            }
        }

        public virtual void AddUser(IdentityUser user)
        {
            if (Users == null)
                Users = new List<IdentityUser>();
            this.Users.Add(user);
        }
    }
}
