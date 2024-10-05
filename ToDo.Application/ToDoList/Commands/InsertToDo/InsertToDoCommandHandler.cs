using ToDo.Application.Common.ApiResult;
using ToDo.Application.Interfaces;

namespace ToDo.Application.ToDoList.Commands.InsertToDo;

public class InsertToDoCommandHandler : IRequestHandler<InsertToDoCommand, ApiResult>
{
    private readonly IUnitOfWork _work;

    public InsertToDoCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }

    public async Task<ApiResult> Handle(InsertToDoCommand request, CancellationToken cancellationToken)
    {
        var toDo = new Domain.Entity.ToDo
        {
            Desc = request.Desc,
            Title = request.Title,
            UserId = request.UserId,
            TimeToDo = request.TimeToDo,
        };
        await _work.GenericRepository<Domain.Entity.ToDo>().AddAsync(toDo, cancellationToken);
        return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
    }
}