using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Product_Services.Infastructure;
using Product_Services.Models;

namespace Product_Services.Repository
{
    public interface IDiscountRepository : IRepository<Discount>
    {

    }
    public class DiscountRepository : Repository<Discount>, IDiscountRepository
    {
        Product_Context _Context;
        public DiscountRepository(Product_Context context) : base(context)
        {
            _Context = context;
        }
        public override void Update(Discount entity)
        {
            Discount target = _Context.Discounts.Where(c=>c.Id==entity.Id).FirstOrDefault();
            _Context.Entry(target).CurrentValues.SetValues(entity);
            _Context.SaveChanges();
        }
    }
}