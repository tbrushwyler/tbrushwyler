using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cumulo.ninja.Core.Domain.Model.Identity
{
    [Serializable]
    public class Claim : EntityBase<Claim>
    {
        public virtual string ClaimType { get; set; }
        public virtual string ClaimValue { get; set; }

        public virtual User User { get; set; }
    }
}
