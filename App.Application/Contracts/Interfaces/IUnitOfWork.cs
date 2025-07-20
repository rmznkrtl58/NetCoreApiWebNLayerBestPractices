namespace App.Application.Contracts.Interfaces
{
    public interface IUnitOfWork
    {
        public Task<int> CommitAsync();
    }
}
