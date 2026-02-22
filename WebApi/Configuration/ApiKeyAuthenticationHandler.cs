using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace WebApi.Configuration;

public class ApiKeyAuthenticationHandler: AuthenticationHandler<AuthenticationSchemeOptions>
{
    public ApiKeyAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder) : base(options, logger, encoder)
    {
    }
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var apikey = Request.Headers.FirstOrDefault(o => o.Key == "x-api-key");

        if(apikey.Value == "111")
        {
            var claims = new[] {
                new Claim(ClaimTypes.Name, "SystemAccount") };

            var identity = new ClaimsIdentity(claims, ApiKeyAuthenticationSсheme.ShemeName);

            var principal = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(principal, ApiKeyAuthenticationSсheme.ShemeName);

            return AuthenticateResult.Success(ticket);
        }
        else
        {
            return AuthenticateResult.Fail("Api key is wrong");
        }
    }
}
