using System;
using System.Collections.Generic;

namespace cumulo.ninja.Core.Domain.Model.Identity
{
    [Serializable]
    public class User : StringEntityBase<User>
    {
        public virtual string UserName { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string SecurityStamp { get; set; }
        public virtual string Email { get; set; }
        public virtual bool IsConfirmedByUser { get; set; }
        public virtual string ConfirmationToken { get; set; }
        public virtual DateTime? PasswordResetTokenCreatedDate { get; set; }
        public virtual bool RequiresPasswordReset { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Claim> Claims { get; set; }
        public virtual ICollection<Login> Logins { get; set; }

        public virtual void AddUserClaim(Claim claim)
        {
            claim.User = this;
            if (Claims == null)
                Claims = new List<Claim>();
            Claims.Add(claim);
        }

        public virtual void AddUserLogin(Login login)
        {
            login.User = this;
            if (Logins == null)
                Logins = new List<Login>();
            Logins.Add(login);
        }

        public virtual void AddRole(Role role)
        {
            if (Roles == null)
                Roles = new List<Role>();
            Roles.Add(role);
            role.AddUser(this);
        }
    }
}
