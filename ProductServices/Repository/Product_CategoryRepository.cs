using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Product_Services.Infastructure;
using Product_Services.Models;

namespace Product_Services.Repository
{
    public interface IProduct_CategoryRepository : IRepository<Product_Category>
    {

    }
    public class Product_CategoryRepository : Repository<Product_Category>, IProduct_CategoryRepository
    {
        Product_Context _Context;
        public Product_CategoryRepository(Product_Context context) : base(context)
        {
            _Context = context;
        }
        public override void Update(Product_Category entity)
        {
            Product_Category target = _Context.Product_Categories.Where(c=>c.Id==entity.Id).FirstOrDefault();
            _Context.Entry(target).CurrentValues.SetValues(entity);
            _Context.SaveChanges();
        }
    }
}