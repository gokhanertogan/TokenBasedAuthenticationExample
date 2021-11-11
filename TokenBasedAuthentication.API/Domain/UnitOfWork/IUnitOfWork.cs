using System.Threading.Tasks;

namespace TokenBasedAuthentication.API.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
        void Complete();
    }
}