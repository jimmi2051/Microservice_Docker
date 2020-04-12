using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rating_services.Infastructure;
using Rating_services.Models;

namespace Rating_services.Repository
{
    public interface IAccountRepository : IRepository<Account>
    {

    }
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(Rating_Context context) : base(context)
        {
        }
    }
}