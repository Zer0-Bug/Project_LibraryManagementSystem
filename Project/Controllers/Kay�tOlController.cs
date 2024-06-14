using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class KayıtOlController : Controller
    {
        // GET: KayıtOl
        DBlibraryEntities db = new DBlibraryEntities();
        [HttpGet]
        public ActionResult Kayıt()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Kayıt(Tbl_üyeler Kayıt)
        {
            if (!ModelState.IsValid)
            {
                return View("Kayıt");
            }
            db.Tbl_üyeler.Add(Kayıt);
            db.SaveChanges();
            return RedirectToAction("LoginIndex", "Login");
        }
    }
}