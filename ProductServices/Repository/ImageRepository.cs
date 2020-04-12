using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Product_Services.Infastructure;
using Product_Services.Models;

namespace Product_Services.Repository
{
    public interface IImageRepository : IRepository<Image>
    {

    }
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        Product_Context _Context;
        public ImageRepository(Product_Context context) : base(context)
        {
            _Context = context;
        }
        public override void Update(Image entity)
        {
            Image target = _Context.Images.Where(c=>c.Id==entity.Id).FirstOrDefault();
            _Context.Entry(target).CurrentValues.SetValues(entity);
            _Context.SaveChanges();
        }
    }
}