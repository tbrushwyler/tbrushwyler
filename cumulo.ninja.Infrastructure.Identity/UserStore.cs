using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using cumulo.ninja.Core.Domain.Model;
using cumulo.ninja.Core.Domain.Model.Identity;
using Microsoft.AspNet.Identity;

namespace cumulo.ninja.Infrastructure.Identity
{
    public class UserStore<TUser> : IUserLoginStore<TUser>, IUserClaimStore<TUser>, IUserRoleStore<TUser>, IUserPasswordStore<TUser>, IUserSecurityStampStore<TUser>, IUserStore<TUser>, IUserEmailStore<TUser>, IDisposable where TUser : IdentityUser
    {
        private bool _disposed = false;

        private IRepository<User> UserRepo { get; set; }
        private IRepository<Role> RoleRepo { get; set; } 
        private IRepository<Login> LoginRepo { get; set; }
        private IRepository<Claim> ClaimRepo { get; set; }

        public UserStore(IRepository<User> userRepo, IRepository<Role> roleRepo, IRepository<Login> loginRepo, IRepository<Claim> claimRepo)
        {
            UserRepo = userRepo;
            RoleRepo = roleRepo;
            LoginRepo = loginRepo;
            ClaimRepo = claimRepo;
        }

        public async Task CreateAsync(TUser user)
        {
            ThrowDisposed();
            if (user == null) throw new ArgumentNullException("user");

            UserRepo.Store(user.BaseType);
        }

        public async Task DeleteAsync(TUser user)
        {
            ThrowDisposed();
            if (user == null) throw new ArgumentNullException("user");

            UserRepo.Delete(user.BaseType);
        }

        public async Task<TUser> FindByIdAsync(string userId)
        {
            ThrowDisposed();
            var user = UserRepo.Get(userId);
            return ConvertUser(user);
        }

        public async Task<TUser> FindByNameAsync(string userName)
        {
            ThrowDisposed();
            var user = UserRepo.FindOneBy(u => u.UserName == userName);
            return ConvertUser(user);
        }

        public async Task UpdateAsync(TUser user)
        {
            ThrowDisposed();
            if (user == null) throw new ArgumentNullException("user");

            UserRepo.Store(user.BaseType);
        }

        public async Task<TUser> FindAsync(UserLoginInfo login)
        {
            ThrowDisposed();
            if (login == null) throw new ArgumentNullException("login");

            var query = from u in UserRepo.Query()
                        from l in u.Logins
                        where l.LoginProvider == login.LoginProvider && l.ProviderKey == login.ProviderKey
                        select u;

            return ConvertUser(query.SingleOrDefault());
        }

        public async Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            ThrowDisposed();
            if (user == null) throw new ArgumentNullException("user");
            if (login == null) throw new ArgumentNullException("login");

            var userLogin = new IdentityUserLogin(new Login()
            {
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider
            });
            user.AddUserLogin(userLogin);

            UserRepo.Store(user.BaseType);
        }

        public async Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            ThrowDisposed();
            if (user == null) throw new ArgumentNullException("user");
            if (login == null) throw new ArgumentNullException("login");

            var info = user.Logins.SingleOrDefault(x => x.LoginProvider == login.LoginProvider && x.ProviderKey == login.ProviderKey);
            if (info != null)
            {
                user.Logins.Remove(info);
                UserRepo.Store(user.BaseType);
            }
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            ThrowDisposed();
            if (user == null) throw new ArgumentNullException("user");

            var result = new List<UserLoginInfo>();
            return user.Logins.Select(x => new UserLoginInfo(x.LoginProvider, x.ProviderKey)).ToList();
        }

        public async Task<IList<System.Security.Claims.Claim>> GetClaimsAsync(TUser user)
        {
            ThrowDisposed();
            if (user == null) throw new ArgumentNullException("user");

            return user.Claims.Select(x => new System.Security.Claims.Claim(x.ClaimType, x.ClaimValue)).ToList();
        }

        public async Task AddClaimAsync(TUser user, System.Security.Claims.Claim claim)
        {   
            ThrowDisposed();
            if (user == null) throw new ArgumentNullException("user");
            if (claim == null) throw new ArgumentNullException("claim");

            var identityClaim = new IdentityUserClaim(new Claim()
            {
                ClaimType = claim.Type,
                ClaimValue = claim.Value
            });
            user.Claims.Add(identityClaim);

            UserRepo.Store(user.BaseType);
        }

        public async Task RemoveClaimAsync(TUser user, System.Security.Claims.Claim claim)
        {
            ThrowDisposed();
            if (user == null) throw new ArgumentNullException("user");
            if (claim == null) throw new ArgumentNullException("claim");

            var identityClaim = user.Claims.SingleOrDefault(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
            if (identityClaim != null)
            {
                user.Claims.Remove(identityClaim);
                UserRepo.Store(user.BaseType);
            }
        }

        public async Task AddToRoleAsync(TUser user, string role)
        {
            ThrowDisposed();
            if (user == null) throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(role)) throw new ArgumentNullException("role");

            var lower = role.ToLowerInvariant();
            var coreRole = RoleRepo.FindOneBy(r => r.Name.ToLowerInvariant() == lower);
            if (coreRole == null)
                throw new InvalidOperationException(string.Format("Role {0} not found", role));

            var identityRole = new IdentityRole(coreRole);
            user.Roles.Add(identityRole);

            UserRepo.Store(user.BaseType);
        }

        public async Task RemoveFromRoleAsync(TUser user, string role)
        {
            ThrowDisposed();
            if (user == null) throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(role)) throw new ArgumentNullException("role");

            var identityRole = user.Roles.SingleOrDefault(x => x.Name.ToLowerInvariant() == role.ToLowerInvariant());
            if (identityRole != null)
            {
                user.Roles.Remove(identityRole);
                UserRepo.Store(user.BaseType);
            }
        }

        public async Task<IList<string>> GetRolesAsync(TUser user)
        {
            ThrowDisposed();
            if (user == null) throw new ArgumentNullException("user");

            return user.Roles.Select(x => x.Name).ToList();
        }

        public async Task<bool> IsInRoleAsync(TUser user, string role)
        {
            ThrowDisposed();
            if (user == null) throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(role)) throw new ArgumentNullException("role");

            return user.Roles.Any(x => x.Name.ToLowerInvariant() == role.ToLowerInvariant());
        }

        public async Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            ThrowDisposed();
            if (user == null) throw new ArgumentNullException("user");

            user.PasswordHash = passwordHash;
            UserRepo.Store(user.BaseType);
        }

        public async Task<string> GetPasswordHashAsync(TUser user)
        {
            ThrowDisposed();
            if (user == null) throw new ArgumentNullException("user");

            return user.PasswordHash;
        }

        public async Task SetSecurityStampAsync(TUser user, string securityStamp)
        {
            ThrowDisposed();
            if (user == null) throw new ArgumentNullException("user");

            user.SecurityStamp = securityStamp;
            UserRepo.Store(user.BaseType);
        }

        public async Task<string> GetSecurityStampAsync(TUser user)
        {
            ThrowDisposed();
            if (user == null) throw new ArgumentNullException("user");

            return user.SecurityStamp;
        }

        public async Task<bool> HasPasswordAsync(TUser user)
        {
            ThrowDisposed();
            if (user == null) throw new ArgumentNullException("user");

            return !string.IsNullOrWhiteSpace(user.PasswordHash);
        }

        public async Task<TUser> FindByEmailAsync(string email)
        {
            ThrowDisposed();
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException("email");

            var lower = email.ToLowerInvariant();
            var user = UserRepo.FindOneBy(u => u.Email.ToLowerInvariant() == lower);
            return ConvertUser(user);
        }

        public async Task<string> GetEmailAsync(TUser user)
        {
            ThrowDisposed();
            if (user == null) throw new ArgumentNullException("user");

            return user.Email;
        }

        public async Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            ThrowDisposed();
            if (user == null) throw new ArgumentNullException("user");

            return user.BaseType.IsConfirmedByUser;
        }

        public async Task SetEmailAsync(TUser user, string email)
        {
            ThrowDisposed();
            if (user == null) throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException("email");

            user.Email = email;
            UserRepo.Store(user.BaseType);
        }

        public async Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            ThrowDisposed();
            if (user == null) throw new ArgumentNullException("user");

            user.BaseType.IsConfirmedByUser = confirmed;
            UserRepo.Store(user.BaseType);
        }

        public void Dispose()
        {
            _disposed = true;
        }

        #region private methods

        /// <summary>
        /// Converts the Core NHibernate domain entity to the ASP.NET IUser object
        /// </summary>
        /// <param name="user">DOmain object</param>
        /// <returns>IUser object</returns>
        private TUser ConvertUser(User user)
        {
            if (user == null)
            {
                return null;
            }
            else
            {
                return new IdentityUser(user) as TUser;
            }
        }

        private void ThrowDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(GetType().Name);
        }

        #endregion
    }
}
