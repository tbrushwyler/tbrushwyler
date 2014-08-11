using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cumulo.ninja.Core.Domain.Model.Identity;
using Microsoft.AspNet.Identity;

namespace cumulo.ninja.Infrastructure.Identity
{
    [Serializable]
    public class IdentityUser : IUser
    {
        public User BaseType { get; set; }

        public IdentityUser(User baseType)
        {
            BaseType = baseType;
        }

        public string Id
        {
            get { return BaseType.Id; }
            set { BaseType.Id = value; }
        }

        public string UserName
        {
            get { return BaseType.UserName; }
            set { BaseType.UserName = value; }
        }

        public string PasswordHash
        {
            get { return this.BaseType.PasswordHash; }
            set { this.BaseType.PasswordHash = value; }
        }

        public string SecurityStamp
        {
            get { return this.BaseType.SecurityStamp; }
            set { this.BaseType.SecurityStamp = value; }
        }

        public DateTime? PasswordResetTokenCreatedDate
        {
            get { return this.BaseType.PasswordResetTokenCreatedDate; }
            set { this.BaseType.PasswordResetTokenCreatedDate = value; }
        }

        public string Email
        {
            get { return this.BaseType.Email; }
            set { this.BaseType.Email = value; }
        }

        public ICollection<IdentityRole> Roles
        {
            get
            {
                ICollection<IdentityRole> roles = new Collection<IdentityRole>();
                if (this.BaseType.Roles != null)
                {
                    foreach (Role role in this.BaseType.Roles)
                    {
                        roles.Add(new IdentityRole(role));
                    }
                }
                return roles;
            }
            protected set
            {
                ICollection<IdentityRole> roles = new Collection<IdentityRole>();
                foreach (IdentityRole role in value)
                {
                    BaseType.AddRole(role.BaseType);
                }
            }
        }

        public ICollection<IdentityUserClaim> Claims
        {
            get
            {
                ICollection<IdentityUserClaim> claims = new Collection<IdentityUserClaim>();
                if (this.BaseType.Claims != null)
                {
                    foreach (Claim claim in this.BaseType.Claims)
                    {
                        claims.Add(new IdentityUserClaim(claim));
                    }
                }
                return claims;
            }
            protected set
            {
                ICollection<IdentityUserClaim> claims = new Collection<IdentityUserClaim>();
                foreach (IdentityUserClaim claim in value)
                {
                    BaseType.AddUserClaim(claim.BaseType);
                }
            }
        }

        public virtual ICollection<IdentityUserLogin> Logins
        {
            get
            {
                ICollection<IdentityUserLogin> claims = new Collection<IdentityUserLogin>();
                if (this.BaseType.Logins != null)
                {
                    foreach (Login login in this.BaseType.Logins)
                    {
                        claims.Add(new IdentityUserLogin(login));
                    }
                }
                return claims;

            }
            protected set
            {
                ICollection<IdentityUserLogin> logins = new Collection<IdentityUserLogin>();
                foreach (var login in value)
                {
                    BaseType.AddUserLogin(login.BaseType);
                }
            }
        }

        public virtual void AddUserClaim(IdentityUserClaim claim)
        {
            claim.User = this;
            if (Claims == null)
                Claims = new List<IdentityUserClaim>();
            Claims.Add(claim);
        }

        public virtual void AddUserLogin(IdentityUserLogin login)
        {
            login.User = this;
            if (Logins == null)
                Logins = new Collection<IdentityUserLogin>();
            Logins.Add(login);
        }

        public virtual void AddRole(IdentityRole role)
        {
            if (Roles == null)
                Roles = new List<IdentityRole>();
            Roles.Add(role);
            role.AddUser(this);
        }
    }
}
