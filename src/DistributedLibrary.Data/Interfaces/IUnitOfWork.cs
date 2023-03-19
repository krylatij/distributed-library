namespace DistributedLibrary.Data.Interfaces;

public interface IUnitOfWork
{
    Task<int> CommitAsync();
}