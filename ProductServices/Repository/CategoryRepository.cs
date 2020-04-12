using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Product_Services.Infastructure;
using Product_Services.Models;

namespace Product_Services.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {

    }
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        Product_Context _Context;
        public CategoryRepository(Product_Context context) : base(context)
        {
            _Context = context;
        }
        public override void Update(Category entity)
        {
            Category target = _Context.Categories.Where(c=>c.Id==entity.Id).FirstOrDefault();
            _Context.Entry(target).CurrentValues.SetValues(entity);
            _Context.SaveChanges();
        }
    }
}