using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Authentication.DataManagment;
using Authentication.DataManagment.BusinessLogicLayer;
using Authentication.Security;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace Authentication.Validators
{
    public class ResourceOwnerPasswordValidator:IResourceOwnerPasswordValidator
    {
        private readonly IUserBL userBL;
        public ResourceOwnerPasswordValidator(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                var user = this.userBL.FindUserByLogin(context.UserName);
                if (user != null)
                {
                    var password = context.Password.HashSHA1();
                    if (user.Password == password)
                    {
                        context.Result = new GrantValidationResult(
                            subject: user.UserId.ToString(),
                            authenticationMethod: "custom",
                            claims: GetUserClaims(user));
                        return;
                    }
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password.");
                    return;
                }
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "That user doesn't exist.");
                return;
            }
            catch(Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
            }
        }
        public static List<Claim> GetUserClaims(UserInfo user)
        {
            return new List<Claim>()
            {
                new Claim("user_id",user.UserId.ToString()??""),
            };
        }
    }
}
