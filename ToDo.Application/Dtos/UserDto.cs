using ToDo.Application.Common.Mapping;

namespace ToDo.Application.Dtos;

public class UserDto:IDto
{
        public string Name { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new List<string>();
}