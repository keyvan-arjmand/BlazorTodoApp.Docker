using ToDo.Application.Common.ApiResult;

namespace ToDo.Application.ToDoList.Commands.DeleteToDo;

public class DeleteToDoCommand:IRequest<ApiResult>
{
    public int Id { get; set; }
}