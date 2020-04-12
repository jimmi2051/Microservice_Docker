using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rating_services.Infastructure;
using Rating_services.Models;

namespace Rating_services.Repository
{
    public interface IRatingRepository : IRepository<Rating>
    {

    }
    public class RatingRepository : Repository<Rating>, IRatingRepository
    {
        Rating_Context _Context;
        public RatingRepository(Rating_Context context) : base(context)
        {
            _Context = context;
        }
        public override void Update(Rating entity)
        {
            Rating target = _Context.Ratings.Where(c => c.Id == entity.Id).FirstOrDefault();
            _Context.Entry(target).CurrentValues.SetValues(entity);
            _Context.SaveChanges();
        }
    }
}