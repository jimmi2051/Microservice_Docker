using Rating_services.Models;

namespace Rating_services.Infastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public Rating_Context Context { get; }

        public UnitOfWork(Rating_Context context)
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
