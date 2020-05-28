using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
    public class Response
    {
        [Key]
        public int ResponseId { get; set; }
        [Required(ErrorMessage = "Enter the title of Response")]
        [StringLength(25, ErrorMessage = "max title string is 25 caracter")]
        [MaxLength(50)]
        public String title_resp { get; set; }
        [DataType(DataType.MultilineText)]
        public String detail_resp { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Response Date")]
        public DateTime date_resp { get; set; }



        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
        public int? QuestionId { get; set; }


    }
}
