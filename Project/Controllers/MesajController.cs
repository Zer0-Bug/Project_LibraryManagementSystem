using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class MesajController : Controller
    {
        // GET: Mesaj
        DBKUTUPHANEEntities1 db = new DBKUTUPHANEEntities1();
        public ActionResult MesajIndex()
        {
            var uyemail = (string)Session["Mail"].ToString();
            var mesajlar = db.Tbl_mesajlar.Where(x => x.Alıcı == uyemail.ToString()).ToList();
            return View(mesajlar);
        }
        public ActionResult Giden()
        {
            var uyemail = (string)Session["Mail"].ToString();
            var mesajlar = db.Tbl_mesajlar.Where(x => x.Gonderen == uyemail.ToString()).ToList();
            return View(mesajlar);
        }
        [HttpGet]
        public ActionResult YeniIndex()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniIndex(Tbl_mesajlar t)
        {
            var uyemail = (string)Session["Mail"].ToString();
            t.Gonderen = uyemail.ToString();
            t.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.Tbl_mesajlar.Add(t);
            db.SaveChanges();
            return RedirectToAction("Giden", "Mesaj");

        }
        public PartialViewResult part1()
        { 
            return PartialView(); 
        }
      
    }
}