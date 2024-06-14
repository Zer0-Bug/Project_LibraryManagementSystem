using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using FrontCode.Libraries;

namespace MvcKutuphane.Controllers
{
    public class PersonelController : Controller
    {
        DBKUTUPHANEEntities1 db = new DBKUTUPHANEEntities1();
        // GET: Personel
        public ActionResult PersonelIndex()
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var degerler = db.Tbl_personel.ToList();
                return View(degerler);
        }
        [HttpGet] 
        public ActionResult PersonelEkle()
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            return View();
        }
        [HttpPost] 
        public ActionResult PersonelEkle(Tbl_personel p)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            if (!ModelState.IsValid)
            {
                return View("PersonelEkle");
            }

            var adminpssw = Hashing.CreateHash(p.PersonelPassword);
            p.PersonelPassword = adminpssw;

            db.Tbl_personel.Add(p);
            db.SaveChanges();
            return RedirectToAction("PersonelIndex");
        }
        public ActionResult PersonelSil(int id)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var prs = db.Tbl_personel.Find(id);
            db.Tbl_personel.Remove(prs);
            db.SaveChanges();
            return RedirectToAction("PersonelIndex"); //beni şu aksiyona yönlendir
        }
        public ActionResult PersonelGetir(int id)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var pers = db.Tbl_personel.Find(id);

            return View("PersonelGetir", pers); //kategori getir sayfasını döndür ama ktg bilgilerini içeren sayfa döndür

        }
        public ActionResult PersonelGuncelle(Tbl_personel p)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var pers = db.Tbl_personel.Find(p.PersonelID);
            pers.PersonelAdSoyad = p.PersonelAdSoyad;
            pers.PersonelEmail = p.PersonelEmail;

            var adminpssw = Hashing.CreateHash(p.PersonelPassword);
            pers.PersonelPassword = adminpssw;

            db.SaveChanges();
            return RedirectToAction("PersonelIndex");
        }
    }
}