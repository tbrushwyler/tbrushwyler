using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cumulo.ninja.Core.Domain.Model;
using cumulo.ninja.Core.Domain.Model.Identity;
using cumulo.ninja.Infrastructure.Identity;

namespace cumulo.ninja.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IRepository<User> UserRepo { get; set; }
        protected IRepository<Login> LoginRepo { get; set; }
        protected IRepository<Claim> ClaimRepo { get; set; }
        protected IRepository<Role> RoleRepo { get; set; }

        protected UserManager<IdentityUser> UserManager { get; set; }

        public BaseController()
        {
            UserRepo = DependencyResolver.Current.GetService<IRepository<User>>();
            LoginRepo = DependencyResolver.Current.GetService<IRepository<Login>>();
            ClaimRepo = DependencyResolver.Current.GetService<IRepository<Claim>>();
            RoleRepo = DependencyResolver.Current.GetService<IRepository<Role>>();

            UserManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(UserRepo, RoleRepo, LoginRepo, ClaimRepo));
        }
    }
}