using ToDo.Application.Common.ApiResult;
using ToDo.Application.Dtos;

namespace ToDo.Application.Users.Queries.GetUserByName;

public class GeUserByNameCommand:IRequest<ApiResult<UserDto>>
{
    public string PhoneNumber { get; set; } = string.Empty;
}