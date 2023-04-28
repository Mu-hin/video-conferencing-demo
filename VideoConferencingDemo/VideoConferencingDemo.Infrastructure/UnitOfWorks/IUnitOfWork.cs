namespace VideoConferencingDemo.Infrastructure.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveAsync();
        Task DisposeAsync();
    }
}
