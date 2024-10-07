using Microsoft.EntityFrameworkCore;
using ToDo.Application.Common.ApiResult;
using ToDo.Application.Common.Mapping;
using ToDo.Application.Dtos;
using ToDo.Application.Interfaces;

namespace ToDo.Application.ToDoList.Queries.GetByIdToDo;

public class GetByIdToDoQueryHandler : IRequestHandler<GetByIdToDoQuery, ApiResult<ToDoDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdToDoQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResult<ToDoDto>> Handle(GetByIdToDoQuery request, CancellationToken cancellationToken)
    {
        var todo = await _unitOfWork.GenericRepository<Domain.Entity.ToDo>().TableNoTracking
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        var dto = todo.ToDto<ToDoDto>();
        return new ApiResult<ToDoDto>(dto, string.Empty, ApiResultStatusCode.Success, true);
    }
}