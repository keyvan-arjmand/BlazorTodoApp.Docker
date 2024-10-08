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
        var token =  await JsRuntime.InvokeAsync<string>("localStorage.getItem", "token");
    
        var user = string.IsNullOrEmpty(token) 
            ? new ClaimsPrincipal(new ClaimsIdentity())
            : new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));
 
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
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}