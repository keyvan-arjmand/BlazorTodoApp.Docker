using Microsoft.AspNetCore.Identity;
using ToDo.Application.Common.ApiResult;
using ToDo.Domain.Entity;

namespace ToDo.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApiResult>
{
    private readonly UserManager<User> _userManager;

    public UpdateUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApiResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.PhoneNumber);
        if (!string.IsNullOrEmpty(request.NewPass))
        {
            bool isSuccess = await _userManager.CheckPasswordAsync(user!, request.OldPass!);
            if (isSuccess)
            {
                await _userManager.CheckPasswordAsync(user, request.NewPass);
            }
            else
            {
                return new ApiResult(string.Empty, ApiResultStatusCode.LogicError, false);
            }
        }

        user.Name = request.Name;
        user.Family = request.Family;
        await _userManager.UpdateAsync(user);
        return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
    }
}