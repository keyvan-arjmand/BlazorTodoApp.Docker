using ToDo.Application.Common.ApiResult;

namespace ToDo.Application.Users.Commands.DeleteUser;

public class DeleteUserCommand:IRequest<ApiResult>
{
    public string PhoneNumber { get; set; } = string.Empty;
}