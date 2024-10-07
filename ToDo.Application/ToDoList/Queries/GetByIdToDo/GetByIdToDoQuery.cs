using ToDo.Application.Common.ApiResult;
using ToDo.Application.Dtos;

namespace ToDo.Application.ToDoList.Queries.GetByIdToDo;

public class GetByIdToDoQuery : IRequest<ApiResult<ToDoDto>>
{
    public int Id { get; set; }
}