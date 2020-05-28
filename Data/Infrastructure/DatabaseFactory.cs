using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {

        // attribut + get + set 

            // private le debut de prop est miniscule
        private piCRMContext dataContext;


        //il public lil get et set mais l atribut est privéé
        public piCRMContext DataContext { get { return dataContext; } }

        public DatabaseFactory()
        {
            dataContext = new piCRMContext();
        }


        //implementation de libération 
        protected override void DisposeCore()
        {
            // libérer espace mémoire du context
            if(DataContext!=null)
            DataContext.Dispose();
        }
    }

}
