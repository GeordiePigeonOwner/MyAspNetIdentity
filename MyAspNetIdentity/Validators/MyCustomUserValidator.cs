using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MyAspNetIdentity.Infrastructure;

namespace MyAspNetIdentity.Validators
{
    public class MyCustomUserValidator : UserValidator<ApplicationUser>
    {
        readonly List<string> _allowedEmailDomains = new List<string> { "outlook.com", "hotmail.com", "gmail.com", "yahoo.com" };

        public MyCustomUserValidator(ApplicationUserManager appUserManager): base(appUserManager)
        {
        }

        public override async Task<IdentityResult> ValidateAsync(ApplicationUser user)
        {
            IdentityResult result = await base.ValidateAsync(user);

            var emailDomain = user.Email.Split('@')[1];

            if (!_allowedEmailDomains.Contains(emailDomain.ToLower()))
            {
                var errors = result.Errors.ToList();

                errors.Add($"Email domain '{emailDomain}' is not allowed");

                result = new IdentityResult(errors);
            }

            return result;
        }
    }
}