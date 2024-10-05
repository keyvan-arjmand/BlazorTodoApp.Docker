using Microsoft.AspNetCore.Identity;
using ToDo.Application.Common.ApiResult;
using ToDo.Domain.Entity;

namespace ToDo.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResult>
{
    private readonly UserManager<User> _userManager;

    public DeleteUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApiResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.PhoneNumber);
        await _userManager.DeleteAsync(user);
        return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
    }
}