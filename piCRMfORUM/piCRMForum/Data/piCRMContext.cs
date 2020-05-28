using Domaine;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
  public  class piCRMContext : DbContext
    {
        public piCRMContext() : base("Name=Alias")
        {
        

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Response> Responses { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {



        }

    }
}
