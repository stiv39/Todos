using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using Todos.Domain.Interfaces.UnitOfWork;

namespace Todos.Infrastructure.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _dbContext;

    public UnitOfWork(DatabaseContext dbContext) => _dbContext = dbContext;

    public Task SaveChangesAsync() =>
        _dbContext.SaveChangesAsync();

    public IDbTransaction BeginTransaction()
    {
        var transaction = _dbContext.Database.BeginTransaction();
        return transaction.GetDbTransaction();
    }
}
