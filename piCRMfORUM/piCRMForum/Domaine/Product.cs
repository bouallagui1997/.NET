using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
    class Product
    {
        public virtual ICollection<Claim> Claims { get; set; }
    }
}
