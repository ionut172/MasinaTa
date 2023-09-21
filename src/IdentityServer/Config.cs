using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("licitatieApp", "Licitatie APP access full")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client {
                ClientId = "postman",
                ClientName = "Postman",
                AllowedScopes = {"openid", "profile", "licitatieApp"},
                RedirectUris = {"https://www.google.com/oauth2/callback"},
                ClientSecrets = new [] {new Secret("NotASecret".Sha256())},
                AllowedGrantTypes = {GrantType.ResourceOwnerPassword}
            }
        };
}
