namespace ToDo.Application.Dtos;

public class CurrentUser
{
    public string? UserName { get; set; }
    public string? UserId { get; set; }
    public List<string>? Roles { get; set; }
}