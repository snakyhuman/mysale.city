using MYSALE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mysalecity.Controllers
{
    public class AppController : Controller
    {
        private mscContext db = new mscContext();
        // GET: App
        public ActionResult Index()
        {
            return View(db.Tasks.ToList());
        }

            public ActionResult Food()
        {
            return View();
        }
            public ActionResult Mood()
        {
            return View();
        }
            public ActionResult Clothes()
        {
            return View();
        }
            public ActionResult Sport()
        {
            return View();
        }
            public ActionResult Education()
        {
            return View();
        }
            public ActionResult Trip()
        {
            return View();
        }
    }
}