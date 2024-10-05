using ToDo.Application.Common.ApiResult;
using ToDo.Application.Interfaces;

namespace ToDo.Application.ToDoList.Commands.UpdateToDo;

public class UpdateToDoCommandHandler : IRequestHandler<UpdateToDoCommand, ApiResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateToDoCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResult> Handle(UpdateToDoCommand request, CancellationToken cancellationToken)
    {
        var toDo = await _unitOfWork.GenericRepository<Domain.Entity.ToDo>()
            .GetByIdAsync(cancellationToken, request.Id);
        toDo.Desc = request.Desc;
        toDo.Title = request.Title;
        toDo.TimeToDo = request.TimeToDo;
        await _unitOfWork.GenericRepository<Domain.Entity.ToDo>().UpdateAsync(toDo, cancellationToken);
        return new ApiResult(string.Empty, ApiResultStatusCode.Success, true);
    }
}