using System;

namespace LogSystem.DAL.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        void Commit();
    }
}
