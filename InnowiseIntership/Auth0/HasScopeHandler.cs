using Microsoft.AspNetCore.Authorization;

namespace InnowiseIntership.Auth0;

#pragma warning disable 
public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
    {
        if(context.User.HasClaim(c => c.Type == "scope" 
                                      && c.Issuer == requirement.Issuer) is false)
            return Task.CompletedTask;
        
        var scopes = context.User.FindFirst(c => c.Type == "scope" 
                                                 && c.Issuer == requirement.Issuer).Value.Split(' ');
        
        if(scopes.Any(s => s == requirement.Scope))
            context.Succeed(requirement);
        return Task.CompletedTask;
    }
}