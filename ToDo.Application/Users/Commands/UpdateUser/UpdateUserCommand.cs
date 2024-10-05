using ToDo.Application.Common.ApiResult;

namespace ToDo.Application.Users.Commands.UpdateUser;

public class UpdateUserCommand:IRequest<ApiResult>
{
    public string PhoneNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Family { get; set; } = string.Empty;
    public string? OldPass { get; set; } 
    public string? NewPass { get; set; } 
}