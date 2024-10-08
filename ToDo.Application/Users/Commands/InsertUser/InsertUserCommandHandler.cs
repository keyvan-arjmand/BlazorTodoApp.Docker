using Microsoft.AspNetCore.Identity;
using ToDo.Application.Common.ApiResult;
using ToDo.Domain.Entity;

namespace ToDo.Application.Users.Commands.InsertUser;

public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand, ApiResult>
{
    private readonly UserManager<User> _userManager;

    public InsertUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApiResult> Handle(InsertUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Name = request.Name,
            Family = request.Family,
            Email = string.Empty,
            PhoneNumber = request.PhoneNumber,
            UserName = request.PhoneNumber,
            SecurityStamp = string.Empty,
            PhoneNumberConfirmed = true,
            EmailConfirmed = true,
            
        };
        await _userManager.CreateAsync(user, request.Pass);
        await _userManager.AddToRoleAsync(user, "User");
        return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
    }
}