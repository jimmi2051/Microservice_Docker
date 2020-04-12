using System;
using Order_servies.Models;
namespace Order_servies.Infastructure
{
    public interface IUnitOfWork : IDisposable
    {
        Order_Context Context { get; }
        void Commit();
    }
}