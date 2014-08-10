using System;
using System.Collections.Generic;

namespace cumulo.ninja.Core.Domain.Model.Identity
{
    [Serializable]
    public class Role : StringEntityBase<Role>
    {
        public virtual string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual void AddUser(User user)
        {
            if (Users == null)
                Users = new List<User>();
            Users.Add(user);
        }
    }
}
