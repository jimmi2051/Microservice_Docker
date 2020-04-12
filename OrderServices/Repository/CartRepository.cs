using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Order_servies.Infastructure;
using Order_servies.Models;

namespace Order_servies.Repository
{
    public interface ICartRepository : IRepository<Cart>
    {

    }
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        Order_Context _Context;
        public CartRepository(Order_Context context) : base(context)
        {
            _Context = context;
        }
        public override void Update(Cart entity)
        {
            Cart target = _Context.Carts.Where(c=>c.Id==entity.Id).FirstOrDefault();
            _Context.Entry(target).CurrentValues.SetValues(entity);
            _Context.SaveChanges();
        }
    }
}