using System.Collections.Generic;
using IdentityServer4.Models;

namespace Authentication
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("userRegistration","UserRegistration")
            };
        }

        public static List<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId="userRegistrationDesktopApp",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256()),
                    },
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    AllowedScopes={"userRegistration","offline_access"},
                    AllowOfflineAccess=true,
                    RefreshTokenUsage=TokenUsage.ReUse,
                },
                new Client
                {
                    ClientId="userRegistration",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedGrantTypes=GrantTypes.ClientCredentials
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
            };
        }
    }
}
