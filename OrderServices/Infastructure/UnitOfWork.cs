using Order_servies.Models;

namespace Order_servies.Infastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public Order_Context Context { get; }

        public UnitOfWork(Order_Context context)
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
