using IdentitySamples.Models.AAA.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentitySamples.Models.Services
{
    public class UserNameNotInPasswordValidator : IPasswordValidator<IdentityUser>
    {
        public virtual async Task<IdentityResult> ValidateAsync(UserManager<IdentityUser> manager, IdentityUser user, string password)
        {
            var errors = new List<IdentityError>();
            if (password.Contains(user.UserName))
            {
                errors.Add(new IdentityError()
                {
                    Code = "UserNameNotInPassword",
                    Description = "Using username in password is Invalid"
                });
            }

            return errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }
    }

    public class BlackListPasswordValidator : UserNameNotInPasswordValidator
    {
        private readonly AAADbContext dbContext;

        public BlackListPasswordValidator(AAADbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public override async Task<IdentityResult> ValidateAsync(UserManager<IdentityUser> manager, IdentityUser user, string password)
        {
            var result = await base.ValidateAsync(manager, user, password);
            var errors = result.Succeeded ? new List<IdentityError>() : result.Errors.ToList();

            var isBlackList = dbContext.blackListPasswordItems.Any(c => c.Value == password);
            if (isBlackList)
            {
                errors.Add(new IdentityError()
                {
                    Code = "PasswordInBlackList",
                    Description = "Using Password That exists in BlackList is invalid"
                });
            }
            return errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }
    }
}
