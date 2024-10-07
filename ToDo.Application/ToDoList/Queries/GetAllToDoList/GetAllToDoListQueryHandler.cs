using Microsoft.EntityFrameworkCore;
using ToDo.Application.Common.ApiResult;
using ToDo.Application.Common.Mapping;
using ToDo.Application.Dtos;
using ToDo.Application.Interfaces;

namespace ToDo.Application.ToDoList.Queries.GetAllToDoList;

public class GetAllToDoListQueryHandler : IRequestHandler<GetAllToDoListQuery, ApiResult<List<ToDoDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllToDoListQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResult<List<ToDoDto>>> Handle(GetAllToDoListQuery request, CancellationToken cancellationToken)
    {
        var toDoList = await _unitOfWork.GenericRepository<Domain.Entity.ToDo>().TableNoTracking
            .Where(x => x.UserId == request.UserId).ToListAsync(cancellationToken);
        return new ApiResult<List<ToDoDto>>(toDoList.ToDto<ToDoDto>().ToList(), string.Empty,
            ApiResultStatusCode.Success, true);
    }
}