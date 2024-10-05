using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Common.ApiResult;
using ToDo.Application.Common.Mapping;
using ToDo.Application.Dtos;
using ToDo.Domain.Entity;

namespace ToDo.Application.Users.Queries.GetUserByName;

public class GeUserByNameCommandHandler : IRequestHandler<GeUserByNameCommand, ApiResult<UserDto>>
{
    private readonly UserManager<User> _userManager;

    public GeUserByNameCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApiResult<UserDto>> Handle(GeUserByNameCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.Include(x => x.ToDoList)
            .FirstOrDefaultAsync(x => x.UserName == request.PhoneNumber, cancellationToken: cancellationToken);
        return new ApiResult<UserDto>(user!.ToDto<UserDto>(), string.Empty, ApiResultStatusCode.Success, true);
    }
}