using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cumulo.ninja.Core.Domain.Model.Identity;

namespace cumulo.ninja.Infrastructure.Identity
{
    [Serializable]
    public class IdentityUserClaim
    {
        public Claim BaseType { get; set; }

        public IdentityUserClaim(Claim baseType)
        {
            BaseType = baseType;
        }

        public string ClaimType
        {
            get { return BaseType.ClaimType; }
            set { BaseType.ClaimType = value; }
        }

        public string ClaimValue
        {
            get { return BaseType.ClaimValue; }
            set { BaseType.ClaimValue = value; }

        }

        public virtual IdentityUser User
        {
            get { return new IdentityUser(this.BaseType.User); }
            set { BaseType.User = value.BaseType; }
        }
    }
}
