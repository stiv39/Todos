using System.Data;

namespace Todos.Domain.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    IDbTransaction BeginTransaction();
    Task SaveChangesAsync();
}
