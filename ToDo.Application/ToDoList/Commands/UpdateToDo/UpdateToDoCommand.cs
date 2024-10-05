using ToDo.Application.Common.ApiResult;

namespace ToDo.Application.ToDoList.Commands.UpdateToDo;

public class UpdateToDoCommand : IRequest<ApiResult>
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Desc { get; set; } = string.Empty;
    public DateTime TimeToDo { get; set; }
}