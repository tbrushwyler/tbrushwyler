using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace cumulo.ninja.Infrastructure.Identity
{
    public class UserManager<TUser> : Microsoft.AspNet.Identity.UserManager<TUser> where TUser : IdentityUser
    {
        public UserManager(IUserStore<TUser> userStore) : base(userStore)
        {
        
        } 
    }
}
