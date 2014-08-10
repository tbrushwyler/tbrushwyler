using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cumulo.ninja.Core.Domain.Model.Identity;

namespace cumulo.ninja.Infrastructure.Identity
{
    [Serializable]
    public class IdentityUserLogin
    {
        public Login BaseType { get; set; }

        public IdentityUserLogin(Login baseType)
        {
            BaseType = baseType;
        }

        public virtual string LoginProvider
        {
            get { return BaseType.LoginProvider; }
            set { BaseType.LoginProvider = value; }
        }

        public virtual string ProviderKey
        {
            get { return BaseType.ProviderKey; }
            set { BaseType.ProviderKey = value; }
        }

        public virtual IdentityUser User
        {
            get { return new IdentityUser(this.BaseType.User); }
            set { BaseType.User = value.BaseType; }
        }
    }
}
