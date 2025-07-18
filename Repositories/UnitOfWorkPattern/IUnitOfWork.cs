

namespace App.Repositories.UnitOfWorkPattern
{
    public interface IUnitOfWork
    {
        public Task<int> CommitAsync();
    }
}
