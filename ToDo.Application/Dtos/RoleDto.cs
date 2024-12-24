using ToDo.Application.Common.Mapping;

namespace ToDo.Application.Dtos;

public class RoleDto:IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}