using Microsoft.EntityFrameworkCore;
using ToDo.Application.Common.ApiResult;
using ToDo.Application.Interfaces;

namespace ToDo.Application.ToDoList.Commands.DeleteToDo;

public class DeleteToDoCommandHandler : IRequestHandler<DeleteToDoCommand, ApiResult>
{
    private readonly IUnitOfWork _work;

    public DeleteToDoCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<ApiResult> Handle(DeleteToDoCommand request, CancellationToken cancellationToken)
    {
        var toDo = await _work.GenericRepository<Domain.Entity.ToDo>().Table
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        await _work.GenericRepository<Domain.Entity.ToDo>().DeleteAsync(toDo!, cancellationToken);
        return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
    }
}