using System.Data;

namespace CutInLine.Repository
{
    public interface IUnitOfWork
    {
        IDbConnection Connection { get; }
        void Begin();
        void Commit();
        void Rollback();
        void Dispose();
        bool IsActive();
    }
}
