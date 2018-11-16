using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Authentication.DataManagment;
using Authentication.DataManagment.BusinessLogicLayer;
using Authentication.Validators;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace Authentication.Services
{
    public class ProfileService:IProfileService
    {
        private IUserBL userBL;

        public ProfileService(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                //depending on the scope accessing the user data.
                if (!string.IsNullOrEmpty(context.Subject.Identity.Name))
                {
                    //get user from db (in my case this is by login)
                    var user = this.userBL.FindUserByLogin(context.Subject.Identity.Name);

                    if (user != null)
                    {
                        //set issued claims to return
                        context.IssuedClaims = GetUserClaims(user);
                    }
                }
                else
                {
                    //get subject from context (this was set ResourceOwnerPasswordValidator.ValidateAsync),
                    //where and subject was set to my user id.
                    var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");

                    if (!string.IsNullOrEmpty(userId?.Value) && long.Parse(userId.Value) > 0)
                    {
                        //get user from db (find user by user id)
                        var user = userBL.FindUserById(int.Parse(userId.Value));

                        // issue the claims for the user
                        if (user != null)
                        {
                            context.IssuedClaims = ResourceOwnerPasswordValidator.GetUserClaims(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //log your error
            }
        }

        public static List<Claim> GetUserClaims(UserInfo user)
        {
            return new List<Claim>()
            {
                new Claim("user_id",user.UserId.ToString()??""),
            };
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                //get subject from context (set in ResourceOwnerPasswordValidator.ValidateAsync),
                var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "user_id");

                if (!string.IsNullOrEmpty(userId?.Value) && int.Parse(userId.Value) > 0)
                {
                    var user = userBL.FindUserById(int.Parse(userId.Value));

                    if (user != null)
                    {
                        context.IsActive = true;
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error logging
            }
        }
    }
}
