using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace cumulo.ninja.Infrastructure.Identity
{
    public class UserStore<TUser> : IUserStore<TUser> where TUser : IdentityUser
    {
        public Task CreateAsync(TUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TUser user)
        {
            throw new NotImplementedException();
        }

        public Task<TUser> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<TUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
