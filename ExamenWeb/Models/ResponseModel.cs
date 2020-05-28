using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ResponseModel
    {

        public int ResponseId { get; set; }
        public String title_resp { get; set; }
        public String detail_resp { get; set; }
        public DateTime date_resp = DateTime.Now;
        public  int? QuestionId { get; set; }
        public string username = "Ayoub";
        public int? UserId = 1;
        public String title_question { get; set; }
        public int Rating =2;
    }
}