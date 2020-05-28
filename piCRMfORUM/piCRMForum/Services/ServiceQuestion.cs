using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Domaine;
using MyFinance.Data.Infrastructure;

namespace Services
{
    public class ServiceQuestion : Service<Question>,IServiceQuestion
    {
        private static DatabaseFactory dbf = new DatabaseFactory();

        private static IUnitOfWork uow = new UnitOfWork(dbf);
        public ServiceQuestion():base(uow)
        {



        }
    }
}
