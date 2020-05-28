using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using System.Web.Mvc;
using Data;
using Domaine;
using Web.Models;
using Services;
using ExamenWeb.Models;


namespace ExamenWeb.Controllers
{
    public class QuestionsController : Controller
    {
        ServiceQuestion sq = new ServiceQuestion();
        ServiceResponse sr = new ServiceResponse();
        private piCRMContext db = new piCRMContext();
        /*
                // GET: Questions
                public ActionResult Index()
                {
                    var questions = db.Questions.Include(q => q.User);
                    return View(questions.ToList());
                }

           */

        /*
                // GET: Questions
                public ActionResult Index()
                {
                    List<QuestionModel> list = new List<QuestionModel>();

                    foreach (var item in sq.GetAll())
                    {
                        QuestionModel qm = new QuestionModel();
                        qm.QuestionId = item.QuestionId;
                        qm.title_question = item.title_question;

                        qm.detail_question = item.detail_question;
                        qm.date_question = item.date_question;


                        qm.categories = item.categories;
                        qm.UserId = item.UserId;


                        list.Add(qm);
                    }
                    return View(list);
                }
            */

  

        // GET: Questions
        public ActionResult Index(String category, String title, String sortOrder)
        {
            List<QuestionModel> list = new List<QuestionModel>();

            foreach (var item in sq.GetAll())
            {
                QuestionModel qm = new QuestionModel();
                qm.QuestionId = item.QuestionId;
                qm.title_question = item.title_question;

                qm.detail_question = item.detail_question;
                qm.date_question = item.date_question;


                qm.categories = item.categories;
                qm.UserId = item.UserId;


                list.Add(qm);
            }


            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "Title_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";


            var questions = from c in list select c;


            switch (sortOrder)
            {
                case "Title_desc":
                    questions = questions.OrderByDescending(s => s.title_question);
                    break;
                case "Date":
                    questions = questions.OrderBy(s => s.date_question);
                    break;
                case "date_desc":
                    questions = questions.OrderByDescending(s => s.date_question);
                    break;
                default:
                    questions = questions.OrderBy(s => s.title_question);
                    break;

            }



            if (!String.IsNullOrEmpty(title))
            {

                questions = questions.Where(c => c.title_question.Contains(title));


            }
            if (!String.IsNullOrEmpty(category))
            {

                questions = questions.Where(c => c.categories.ToString() == category);


            }

            return View(questions);

        }

        /*  

      // GET: Questions
      public ActionResult Index(String category,String title)
      {
          List<QuestionModel> list = new List<QuestionModel>();

          foreach (var item in sq.GetAll())
          {
              QuestionModel qm = new QuestionModel();
              qm.QuestionId = item.QuestionId;
              qm.title_question = item.title_question;

              qm.detail_question = item.detail_question;
              qm.date_question = item.date_question;


              qm.categories = item.categories;
              qm.UserId = item.UserId;


              list.Add(qm);
          }
          var questions = from c in list select c;

          if(!String.IsNullOrEmpty(title))
          {

              questions = questions.Where(c => c.title_question.Contains(title));


          }
       if (!String.IsNullOrEmpty(category))
          {

              questions = questions.Where(c => c.categories.ToString() == category);


          }

          return View(questions);

      }
      */


        /* 
         // GET: Questions/Details/5
         public ActionResult Details(int? id)
         {
             if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             Question question = db.Questions.Find(id);
             if (question == null)
             {
                 return HttpNotFound();
             }
             return View(question);
         }
         */


        // GET: Questions/Details/5
        public ActionResult Details(int id)
        {
            Question q = new Question();
            q = sq.Get(t => t.QuestionId == id);
            QuestionModel qm = new QuestionModel();
            qm.QuestionId = q.QuestionId;
            ViewBag.QuestionId = id;
            qm.username = "Ayoub";
            qm.title_question = q.title_question;
            qm.detail_question = q.detail_question;
            qm.date_question = q.date_question;
            qm.categories=q.categories;
            return View(qm);
        }
/*
        // GET: Questions/DetailsResponse/5
        public ActionResult DetailsResponse(int id)
        {
            Response r = new Response();
            r = sr.Get(t => t.ResponseId == id);
            ResponseModel rm = new ResponseModel();
            rm.QuestionId = r.QuestionId;
            ViewBag.QuestionId = id;
            rm.username = "Ayoub";
            rm.title_question = r.title_question;
            qm.detail_question = q.detail_question;
            qm.date_question = q.date_question;
            qm.categories = q.categories;
            return View(qm);
        }

*/
        // GET: Questions/CreateResponse

        public ActionResult CreateResponse( int id, String title_quest)
        {
            ResponseModel rm = new ResponseModel();
            rm.QuestionId = id;
            rm.title_question = title_quest;


            return View(rm);
        }

      


        // GET: Questions/createQuestion

        public ActionResult createQuestion()
        {
           QuestionModel qm = new QuestionModel();

            //pour recupérer un  champ spécifique  Select(c=>c.name)
            //pour recuperer une liste
           // qm.categories = sc.GetMany().Select(c => new SelectListItem() { Text = c.Name, Value = c.CategoryId.ToString() });

            return View(qm);
        }



        // POST: Questions/createQuestion
        [HttpPost]
        public ActionResult createQuestion(QuestionModel qm)
        {

            Question q = new Question()
            {

                title_question = qm.title_question,

                detail_question = qm.detail_question,
                date_question = qm.date_question,


                categories = qm.categories,
                UserId=qm.UserId
               

            };
          
        
            try
            {
                // TODO: Add insert logic here
                sq.Add(q);
                // listprod.Add(p);

                // Session["Products"] = listprod;

                sq.Commit();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        /*
        // GET: Questions/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, 1, "Nom");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuestionId,title_question,detail_question,date_question,categories,UserId")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "Nom", question.UserId);
            return View(question);
        }

    
    */
        /*
             // GET: Questions/Edit/5
             public ActionResult Edit(int? id)
             {
                 if (id == null)
                 {
                     return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                 }



                 Question question = db.Questions.Find(id);
                 if (question == null)
                 {
                     return HttpNotFound();
                 }


                   ViewBag.UserId = new SelectList(db.Users, "UserId", "Nom", question.UserId);
                   return View(question);
             }

             // POST: Questions/Edit/5
             // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
             // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

                     [HttpPost]
                     [ValidateAntiForgeryToken]
                     public ActionResult Edit([Bind(Include = "QuestionId,title_question,detail_question,date_question,categories,UserId")] Question question)
                     {
                         if (ModelState.IsValid)
                         {
                             db.Entry(question).State = EntityState.Modified;
                             db.SaveChanges();
                             return RedirectToAction("Index");
                         }
                         ViewBag.UserId = new SelectList(db.Users, "UserId", "Nom", question.UserId);
                         return View(question);
                     }

                 */

        /*
        // GET: Questions/EditQuestion
        public ActionResult EditQuestion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }


          
        }

        // POST: Questions/EditQuestion
        
       
                [HttpPost]
                
                public ActionResult EditQuestion(QuestionModel qm)
                {


            Question q = new Question()
            {
                detail_question = qm.detail_question
            };

            try
            {
                // TODO: Add insert logic here
                sq.Update(q);
                // listprod.Add(p);

                // Session["Products"] = listprod;

                sq.Commit();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }



        }
                
    */


        // GET: Questions/Edit/5
        public ActionResult Edit(int id)
        {

            Question q = new Question();
            q = sq.Get(t => t.QuestionId == id);
            QuestionModel qm = new QuestionModel();
            qm.QuestionId = id;
           
        //    qm.title_question = q.title_question;
            qm.detail_question = q.detail_question;
            qm.date_question = q.date_question;
            //qm.categories = q.categories;
            //qm.UserId = q.UserId;
            return View(qm);

        }
        // POST: Questions/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, QuestionModel qm)
        {
            Question q = new Question();
            q = sq.Get(t => t.QuestionId == id);
          
          
           // q.title_question = qm.title_question;
            q.detail_question = qm.detail_question;
            q.date_question = qm.date_question;
            //q.categories = qm.categories;
            //q.UserId = qm.UserId;
            sq.Update(q);
            sq.Commit();
            return RedirectToAction("Index");
        }


        // GET: Responses/Edit/5
        public ActionResult EditResponse(int id)

        {
            ResponseModel rm = new ResponseModel();
            rm.ResponseId = id;
            


            return View(rm);

        }

        // GET: Responses/Delete/5
        public ActionResult DeleteResponse(int id)

        {


            ResponseModel rm = new ResponseModel();
     
            
         
            rm.ResponseId = id;


            return View(rm);

        }




        // GET: Questions/Delete/5
        public ActionResult Delete(int id)
        {
            Question q = new Question();
            q = sq.Get(t => t.QuestionId == id);
            QuestionModel qm = new QuestionModel();
            qm.QuestionId = id;
            qm.title_question = q.title_question;

            qm.detail_question = q.detail_question;
            qm.date_question = q.date_question;


            qm.categories = q.categories;
            qm.UserId = q.UserId;

            return View(qm);
        }

        

        // POST: Questions/Delete/5
        [HttpPost]
                public ActionResult Delete(int id, QuestionModel qm)
                {
                    Question q = new Question();
                    q = sq.Get(t => t.QuestionId == id);
                    sq.Delete(q);
                    sq.Commit();
                    return RedirectToAction("Index");
                }
        /*
        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Question question = db.Questions.Find(id);
                if (question == null)
                {
                    return HttpNotFound();
                }
                return View(question);
            }

            // POST: Questions/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                Question question = db.Questions.Find(id);
                db.Questions.Remove(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
    */




        // GET: Questions/ListResponse
        public ActionResult ListResponse(int id, String title_question)
        {


     List<ResponseModel> list = new List<ResponseModel>();
            foreach (var item in sr.GetMany((t => t.QuestionId == id)))

            {
                //condition si ne trouve pas des question dans item: redirect to page vide ou erreur


                
                ResponseModel rm = new ResponseModel();
             
                rm.QuestionId = id;
                rm.title_question = title_question;
                rm.ResponseId = item.ResponseId;
                rm.title_resp = item.title_resp;

                rm.detail_resp = item.detail_resp;
                rm.date_resp = item.date_resp;

                
                rm.username = "Ayoub";
                rm.UserId = 1;


                list.Add(rm);
                
                
            }



            return View(list);
            

    }


        // GET: Questions/bar
        public ActionResult bar()
        {

            

             List<DataPoint> dataPoints = new List<DataPoint>();

             var Question = (from x in db.Questions select x);
             foreach (var item in Question)
             {
                 // var Responses = (from x in db.Responses where x.QuestionId==item.QuestionId select x);
                 List<Response> list = new List<Response>();

                 foreach (var item1 in sr.GetMany((t => t.QuestionId ==item.QuestionId)))

                 {
                     list.Add(item1);

                 }

                 //int nbr = (from x in Responses select x).Count();


                    dataPoints.Add(new DataPoint(item.title_question,list.Count()));


             }
          
             

  

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
        
            return View();

        }


        // GET: Questions/Pie
        public ActionResult Pie()
        {


            List<QuestionModel> list = new List<QuestionModel>();

            foreach (var item in sq.GetAll())
            {
                QuestionModel qm = new QuestionModel();
                qm.QuestionId = item.QuestionId;
                qm.title_question = item.title_question;

                qm.detail_question = item.detail_question;
                qm.date_question = item.date_question;


                qm.categories = item.categories;
                qm.UserId = item.UserId;


                list.Add(qm);
            }


            var questions = from c in list select c;
            int nbr_question = (from x in list select x).Count();
                int nbr_product= (from x in list where x.categories.ToString()=="Product" select x).Count();

           int nbr_tecnical = (from x in list where x.categories.ToString() == "Technical" select x).Count();
            int nbr_service = (from x in list where x.categories.ToString() == "Service" select x).Count();
            int nbr_finance = (from x in list where x.categories.ToString() == "Finance" select x).Count();
            List<DataPoint> dataPoints = new List<DataPoint>();

            dataPoints.Add(new DataPoint("Product", (nbr_product*100)/ nbr_question));
            dataPoints.Add(new DataPoint("Tecnical", (nbr_tecnical*100)/ nbr_question));
            dataPoints.Add(new DataPoint("Service", (nbr_service * 100) / nbr_question));
            dataPoints.Add(new DataPoint("Finance", (nbr_finance * 100) / nbr_question));
      

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
        }





        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }




    }
}
