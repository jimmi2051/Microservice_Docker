using System;
using Rating_services.Models;
namespace Rating_services.Infastructure
{
    public interface IUnitOfWork : IDisposable
    {
        Rating_Context Context { get; }
        void Commit();
    }
}