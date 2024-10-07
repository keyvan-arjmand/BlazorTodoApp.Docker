using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ToDo.Application.Common.Utilities;
using ToDo.Domain.Entity;

namespace ToDo.Ui;

public class Helper
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public Helper(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitializeAsync()
    {
        var adminUser = await _userManager.FindByEmailAsync("admin");
        if (adminUser == null)
        {
            adminUser = new User
            {
                Email = "admin",
                PhoneNumber = "admin",
                EmailConfirmed = true,
                SecurityStamp = string.Empty,
                UserName = "admin",
                PhoneNumberConfirmed = true,
            };
            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new Role
                {
                    Name = "User"
                });
            }

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new Role
                {
                    Name = "Admin"
                });
            }

            await _userManager.CreateAsync(adminUser, "admin");
            await _userManager.AddToRoleAsync(adminUser, "Admin");
            await _userManager.AddToRoleAsync(adminUser, "User");
        }
    }

    public string GenerateJwtToken(User user, List<string> role)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaims(new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
        });

        foreach (var i in role)
        {
            claims.AddClaim(new Claim(ClaimTypes.Role, i));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("15398f7c1b2b61e85f76c2c50c203ec2"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims.Claims,
            expires: DateTime.Now.AddYears(1),
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}