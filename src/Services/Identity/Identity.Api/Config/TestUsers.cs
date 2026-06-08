using System.Security.Claims;
using Duende.IdentityServer.Test;

namespace Identity.Api.Config;

public static class TestUsers
{
    public static List<TestUser> Users =>
        new()
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "alice",
                Password = "alice",
                Claims = new List<Claim>
                {
                    new("name", "Alice"),
                    new("email", "alice@test.com"),
                },
            },
            new TestUser
            {
                SubjectId = "2",
                Username = "bob",
                Password = "bob",
                Claims = new List<Claim>
                {
                    new("name", "Bob"),
                    new("email", "bob@test.com"),
                },
            },
        };
}
