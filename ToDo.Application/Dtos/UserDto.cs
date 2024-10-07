using ToDo.Application.Common.Mapping;

namespace ToDo.Application.Dtos;

public class UserDto:IDto
{
        public List<string> Roles { get; set; } = new List<string>();
}