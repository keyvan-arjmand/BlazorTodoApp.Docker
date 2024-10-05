using ToDo.Application.Common.ApiResult;

namespace ToDo.Application.Users.Commands.InsertUser;

public class InsertUserCommand:IRequest<ApiResult>
{
    public string Name { get; set; } = string.Empty;
    public string Family { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Pass { get; set; } = string.Empty;
}