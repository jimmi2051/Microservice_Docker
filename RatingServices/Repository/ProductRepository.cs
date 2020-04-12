using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rating_services.Infastructure;
using Rating_services.Models;

namespace Rating_services.Repository
{
    public interface IProductRepository : IRepository<Product>
    {

    }
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        Rating_Context _Context;
        public ProductRepository(Rating_Context context) : base(context)
        {
            _Context = context;
        }
        public override void Update(Product entity)
        {
            Product target = _Context.Products.Where(c=>c.Id==entity.Id).FirstOrDefault();
            _Context.Entry(target).CurrentValues.SetValues(entity);
            _Context.SaveChanges();
        }
    }
}