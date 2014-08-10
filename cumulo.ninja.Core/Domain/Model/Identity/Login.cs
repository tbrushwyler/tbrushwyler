using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cumulo.ninja.Core.Domain.Model.Identity
{
    [Serializable]
    public class Login : EntityBase<Login>
    {
        public virtual string LoginProvider { get; set; }
        public virtual string ProviderKey { get; set; }
        public virtual User User { get; set; }
    }
}
