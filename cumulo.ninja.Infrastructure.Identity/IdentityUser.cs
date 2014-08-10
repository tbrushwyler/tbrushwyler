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
        private ICollection<IdentityUserClaim> _logins;

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
                    this.BaseType.Roles.Add(role.BaseType);
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
                    this.BaseType.Claims.Add(claim.BaseType);
                }
            }
        }

        public virtual ICollection<IdentityUserClaim> Logins
        {
            get
            {
                if (this._logins == null)
                {
                    this._logins = new Collection<IdentityUserClaim>();
                }
                return _logins;

            }
            protected set { this._logins = value; }
        }

        public virtual void AddUserClaim(IdentityUserClaim claim)
        {
            claim.User = this;
            if (Claims == null)
                Claims = new List<IdentityUserClaim>();
            Claims.Add(claim);
        }

        public virtual void AddUserLogin(IdentityUserClaim login)
        {
            login.User = this;
            if (Logins == null)
                Logins = new Collection<IdentityUserClaim>();
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
