using System;
using Product_Services.Models;
namespace Product_Services.Infastructure
{
    public interface IUnitOfWork : IDisposable
    {
        Product_Context Context { get; }
        void Commit();
    }
}