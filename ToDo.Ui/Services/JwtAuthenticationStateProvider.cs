using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;

namespace ToDo.Ui.Services;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedSessionStorage _protectedSessionStorage;
    private readonly IJSRuntime JsRuntime;

    public JwtAuthenticationStateProvider(ProtectedSessionStorage protectedSessionStorage, IJSRuntime jsRuntime)
    {
        _protectedSessionStorage = protectedSessionStorage;
        JsRuntime = jsRuntime;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await JsRuntime.InvokeAsync<string>("localStorage.getItem", "token");

        ClaimsPrincipal user;

        if (string.IsNullOrEmpty(token))
        {
            user = new ClaimsPrincipal(new ClaimsIdentity());
        }
        else
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub,
                        jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value ??
                        string.Empty),
                    new Claim(JwtRegisteredClaimNames.NameId,
                        jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value ??
                        string.Empty)
                };

                var roles = jwtToken.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var identity = new ClaimsIdentity(claims, "jwt");

                user = new ClaimsPrincipal(identity);
            }
            catch (Exception)
            {
                user = new ClaimsPrincipal(new ClaimsIdentity());
            }
        }

        return new AuthenticationState(user);
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1]; // استخراج بخش Payload
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
}