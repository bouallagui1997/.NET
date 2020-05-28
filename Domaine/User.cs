using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
    public class User
    {

        [Key]
        public int UserId { get; set; }
        public String Nom { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
