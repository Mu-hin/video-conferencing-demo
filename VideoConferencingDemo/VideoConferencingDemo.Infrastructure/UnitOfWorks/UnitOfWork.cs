using Microsoft.EntityFrameworkCore;

namespace VideoConferencingDemo.Infrastructure.UnitOfWorks
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        protected readonly DbContext _dbContext;

        public UnitOfWork(DbContext dbContext) => _dbContext = dbContext;

        public async virtual Task SaveAsync() => await _dbContext.SaveChangesAsync();
        public async virtual Task DisposeAsync() => await _dbContext.DisposeAsync();
        public virtual void Dispose() => _dbContext?.Dispose();
    }
}
