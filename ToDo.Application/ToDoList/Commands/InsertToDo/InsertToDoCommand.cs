using ToDo.Application.Common.ApiResult;

namespace ToDo.Application.ToDoList.Commands.InsertToDo;

public class InsertToDoCommand : IRequest<ApiResult>
{
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Desc { get; set; } = string.Empty;
    public DateTime TimeToDo { get; set; }
}