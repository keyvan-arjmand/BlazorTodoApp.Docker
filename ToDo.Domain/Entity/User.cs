using Microsoft.AspNetCore.Identity;
using ToDo.Domain.Common;

namespace ToDo.Domain.Entity;

public class User : IdentityUser<int>
{
    public string Name { get; set; } = string.Empty;
    public string Family { get; set; } = string.Empty;
    public ICollection<ToDo> ToDoList { get; set; } = default!;
}