using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rating_services.Infastructure;
using Rating_services.Models;

namespace Rating_services.Repository
{
    public interface IOrder_detailRepository : IRepository<Order_detail>
    {

    }
    public class Order_detailRepository : Repository<Order_detail>, IOrder_detailRepository
    {
        public Order_detailRepository(Rating_Context context) : base(context)
        {
        }
    }
}