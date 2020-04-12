using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Order_servies.Infastructure;
using Order_servies.Models;

namespace Order_servies.Repository
{
    public interface IOrder_detailRepository : IRepository<Order_detail>
    {

    }
    public class Order_detailRepository : Repository<Order_detail>, IOrder_detailRepository
    {
        public Order_detailRepository(Order_Context context) : base(context)
        {
        }
    }
}