using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Exceptions.NotFound;
using InnowiseIntership;
using Microsoft.Extensions.Configuration;
using Repository.Contracts;
using Service.Contracts;
using Shared.Input;


namespace Service;

#pragma warning disable
public class LoginService(IRepositoryManager repositoryManager,IConfiguration configuration,ICertificate certificate) : ILoginService
{
    private readonly string _domain = $"https://{configuration.GetSection("Auth0").GetSection("Domain").Value}/";
    private readonly string _clientId = configuration.GetSection("Auth0").GetSection("ClientId").Value;
    private readonly string _audience = configuration.GetSection("Auth0").GetSection("Audience").Value;
    
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly ICertificate _certificate = certificate;
    
    public async Task<string> LoginAsync(LoginDto login)
    {
        var user = await _repositoryManager.User.GetUserByEmailAsync(login.Email,false);
        if(user == null)
            throw new UserNotFoundException(login.Email);

        var auth0Client = new AuthenticationApiClient(_domain);
        var request = new ClientCredentialsTokenRequest
        {
            ClientId = _clientId,
            Audience = _audience,
            ClientAssertionSecurityKey = _certificate.Rsa,
            ClientAssertionSecurityKeyAlgorithm = "RS256",
            //does not work -> Scope = user.Admin? "CRUD:users" : ""
        };
        

        var tokenResponse = await auth0Client.GetTokenAsync(request);
        return tokenResponse.AccessToken;

    }
}