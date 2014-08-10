using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace cumulo.ninja.Infrastructure.Identity
{
    public class PasswordValidator : IIdentityValidator<string>
    {
        public int RequiredLength { get; set; }
        public PasswordValidator(int length)
        {
            RequiredLength = length;
        }

        public Task<IdentityResult> ValidateAsync(string item)
        {
            if (String.IsNullOrEmpty(item) || item.Length < RequiredLength)
            {
                return Task.FromResult(IdentityResult.Failed(String.Format("Password should be of length {0}", RequiredLength)));
            }

            string pattern = @"^(?=.*[0-9])(?=.*[!@#$%^&*])[0-9a-zA-Z!@#$%^&*0-9]{10,}$";

            if (!Regex.IsMatch(item, pattern))
            {
                return Task.FromResult(IdentityResult.Failed("Password should have one numeral and one special character"));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
