using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data;
using Domaine;
using Web.Models;
using Services;
using System.Net.Mail;
using System.Net;

namespace ExamenWeb.Controllers
{
    public class ResponsesController : Controller
    {
        private piCRMContext db = new piCRMContext();
        ServiceResponse sr = new ServiceResponse();

        // GET: Responses
        public ActionResult Index()
        {
            var responses = db.Responses.Include(r => r.Question);
            return View(responses.ToList());
        }

        // GET: Responses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            return View(response);
        }



        // POST: Responses/Create
        [HttpPost]
        public ActionResult create( ResponseModel rm,String title_question)
        {

            Response r = new Response()

            {
                QuestionId=rm.QuestionId,
                
                title_resp = rm.title_resp,
                
                detail_resp = rm.detail_resp,
                date_resp = rm.date_resp


            };

           
            try
            {
                // TODO: Add insert logic here
                sr.Add(r);
  

                sr.Commit();
                return RedirectToAction("ListResponse", "Questions", new { id = r.QuestionId, title_question=title_question });
               // return RedirectToAction("ResponsesByQuestionId", new { idq = r.QuestionId });  
            }
            catch
            {
                return View();
            }
        }


        // GET: Responses/Edit/5
        public ActionResult Edit(int id)
        {
            Response r = new Response();
            ResponseModel rm = new ResponseModel();
            r = sr.Get(t => t.ResponseId == id);
            rm.ResponseId = id;
            rm.title_resp = r.title_resp;
            rm.detail_resp = r.detail_resp;
            rm.date_resp = r.date_resp;
            rm.username = "Ayoub";
            

            return View(rm);
        }




        // POST: Responses/Edit/5
        [HttpPost]
        public ActionResult Edit(ResponseModel rm)
        {
            Response r = new Response();
            r = sr.Get(t => t.ResponseId == rm.ResponseId);
            r.ResponseId = rm.ResponseId;
            r.detail_resp = rm.detail_resp;
            r.date_resp = rm.date_resp;
               
           
                // TODO: Add insert logic here
                sr.Update(r);




                sr.Commit();
          return RedirectToAction("Details", new { id = r.ResponseId });



        }

        /*
        // GET: Responses/Create
        public ActionResult Create()
        {
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "title_question");
            return View();
        }

        // POST: Responses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResponseId,title_resp,detail_resp,date_resp,QuestionId")] Response response)
        {
            if (ModelState.IsValid)
            {
                db.Responses.Add(response);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "title_question", response.QuestionId);
            return View(response);
        }
        */
        /*
        // GET: Responses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "title_question", response.QuestionId);
            return View(response);
        }

        // POST: Responses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResponseId,title_resp,detail_resp,date_resp,QuestionId")] Response response)
        {
            if (ModelState.IsValid)
            {
                db.Entry(response).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "title_question", response.QuestionId);
            return View(response);
        }
       


        // GET: Responses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            return View(response);
        }

        // POST: Responses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Response response = db.Responses.Find(id);
            db.Responses.Remove(response);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
         */


        // GET: Responses/Delete/5
        public ActionResult Delete(int id)
        {
            
            Response r = new Response();
            r = sr.Get(t => t.ResponseId == id);
            ResponseModel rm = new ResponseModel();
      
            rm.ResponseId = id;
            rm.title_resp = r.title_resp;
            rm.detail_resp = r.detail_resp;
            rm.date_resp = r.date_resp;
            //  rm.username = "Ayoub";
            rm.QuestionId = r.QuestionId;


            return View(rm);
        }

    
        // POST: Responses/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, ResponseModel rm)
        {
            Response r = new Response();
            r = sr.Get(t => t.ResponseId == id);
          
            sr.Delete(r);
            sr.Commit();
            r.QuestionId = rm.QuestionId;
            
            r.title_resp = rm.title_resp;

            r.detail_resp = rm.detail_resp;
            r.date_resp = rm.date_resp;

            //return RedirectToAction("Index");
            // return RedirectToAction("ListResponse", "Questions", new { id = r.ResponseId});
            /****Mail****/
            try
            {
                MailMessage message = new MailMessage("bouallaguiayoub1997@gmail.com", "ayoub.bouallagui@esprit.tn");
               
                message.Subject = "ALERT!!!!";
                message.Body = "hello, your response has being deleting for some reasons, please contact the admin to get more informations :)";
                message.IsBodyHtml = false;

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
            

                NetworkCredential nc = new NetworkCredential("bouallaguiayoub1997@gmail.com", "bouallagui1997");
                client.UseDefaultCredentials = true;
                client.Credentials = nc;
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            /****Mail****/
           
            return RedirectToAction("Index", "Questions");
          

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
