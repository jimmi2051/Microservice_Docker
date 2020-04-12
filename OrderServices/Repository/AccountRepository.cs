using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Order_servies.Infastructure;
using Order_servies.Models;

namespace Order_servies.Repository
{
    public interface IAccountRepository : IRepository<Account>
    {

    }
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(Order_Context context) : base(context)
        {
        }
    }
}