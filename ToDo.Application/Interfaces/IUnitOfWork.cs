using ToDo.Domain.Common;

namespace ToDo.Application.Interfaces;

public interface IUnitOfWork
{
    IGenericRepository<T> GenericRepository<T>() where T : BaseEntity;
}