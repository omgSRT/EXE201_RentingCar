using BusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;

namespace SchoolManagementAPI.Identity;

public class HasScopeRequirement : IAuthorizationRequirement
{
    public string? Issuer { get; set; }
    public string Scope { get; set; } = "";

    public string Role { get; set; } = "";

    public HasScopeRequirement(string scope, string issuer, string role)
    {
        Scope = scope ?? throw new ArgumentNullException(nameof(scope));
        Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        Role = role ?? throw new ArgumentNullException(nameof(role));
    }
}
