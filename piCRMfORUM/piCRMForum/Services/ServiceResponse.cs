using Domaine;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using MyFinance.Data.Infrastructure;

namespace Services
{
    public class ServiceResponse : Service<Response>, IServiceResponse
    {
        private static DatabaseFactory dbf = new DatabaseFactory();
        private static IUnitOfWork uow = new UnitOfWork(dbf);

        public ServiceResponse():base(uow)
        {



        }
    }
}

