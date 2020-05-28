using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
   public class Question
    {

        [Key]
        public int QuestionId { get; set; }

        [Required(ErrorMessage = "Enter the title of question")]
        [StringLength(25, ErrorMessage = "max title string is 25 caracter")]
        [MaxLength(50)]
        public String title_question { get; set; }

        [DataType(DataType.MultilineText)]
        public String detail_question { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Question Date")]
        public DateTime date_question { get; set; }
        public category categories { get; set; }
       
        public enum category
        {
            Technical = 0,
            Service,
            Finance,
            Product

        }
       
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int? UserId { get; set; }
        public virtual ICollection<Response> List_Response { get; set; }


    }

}

