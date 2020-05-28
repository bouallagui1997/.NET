using Domaine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Domaine.Question;

namespace Web.Models
{
    public class QuestionModel
    {
        
        public  int QuestionId { get; set; }

        public String title_question { get; set; }

        public String detail_question { get; set; }

        [DataType(DataType.Date)]
 
        public DateTime date_question = DateTime.Now;

     
        public category categories { get; set; }

        public string username = "Ayoub";
    public int? UserId =1;
        public virtual ICollection<Response> List_Response { get; set; }

    }
}