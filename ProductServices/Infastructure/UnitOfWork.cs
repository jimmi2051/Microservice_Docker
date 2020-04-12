using Product_Services.Models;

namespace Product_Services.Infastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public Product_Context Context { get; }

        public UnitOfWork(Product_Context context)
        {
            Context = context;
        }
        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();

        }
    }
}
