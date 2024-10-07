using ToDo.Application.Common.ApiResult;
using ToDo.Application.Dtos;

namespace ToDo.Application.ToDoList.Queries.GetAllToDoList;

public class GetAllToDoListQuery:IRequest<ApiResult<List<ToDoDto>>>
{
    public int UserId { get; set; }
}