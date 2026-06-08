using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Identity.Api.Config;

public static class IdentityServerConfig
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("eshop.api", "EShop API"),
            new ApiScope("catalog", "Catalog API"),
            new ApiScope("basket", "Basket API"),
            new ApiScope("ordering", "Ordering API"),
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new ApiResource("eshop", "EShop APIs")
            {
                Scopes = { "eshop.api", "catalog", "basket", "ordering" },
            },
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "eshop.client",
                ClientName = "EShop Client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "eshop.api",
                    "catalog",
                    "basket",
                    "ordering",
                },
            },
            new Client
            {
                ClientId = "eshop.m2m",
                ClientName = "EShop Machine to Machine",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("m2m_secret".Sha256()) },
                AllowedScopes = { "eshop.api", "catalog", "basket", "ordering" },
            },
        };
}
