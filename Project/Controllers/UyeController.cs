using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using PagedList;
using PagedList.Mvc;
using FrontCode.Libraries;

namespace MvcKutuphane.Controllers
{
    public class UyeController : Controller
    {
        // GET: Uye
        DBKUTUPHANEEntities1 db = new DBKUTUPHANEEntities1();
        public ActionResult UyeIndex(int sayfa = 1)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            //var degerler = db.Tbl_üyeler.ToList();
            var degerler = db.Tbl_üyeler.ToList().ToPagedList(sayfa, 3);
            return View(degerler);
        }
        [HttpGet] //sayfa yüklemek istediğimiz zaman bu başka işlem yapmıyosak
        public ActionResult UyeEkle()
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }

            return View();
        }
        [HttpPost] // herhangi ekleme işlemi yapılınca bu
        public ActionResult UyeEkle(Tbl_üyeler p)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            if (!ModelState.IsValid)
            {
                return View("UyeEkle");
            }
            var uyepssw = Hashing.CreateHash(p.ÜyeSifre);
            p.ÜyeSifre = uyepssw;

            db.Tbl_üyeler.Add(p);
            db.SaveChanges();
            return RedirectToAction("UyeIndex");
        }
        public ActionResult UyeSil(int id)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var prs = db.Tbl_üyeler.Find(id);
            db.Tbl_üyeler.Remove(prs);
            db.SaveChanges();
            return RedirectToAction("UyeIndex"); //beni şu aksiyona yönlendir
        }
        public ActionResult UyeGetir(int id)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var pers = db.Tbl_üyeler.Find(id);
            return View("UyeGetir", pers); //kategori getir sayfasını döndür ama ktg bilgilerini içeren sayfa döndür

        }
        public ActionResult UyeGuncelle(Tbl_üyeler p)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var pers = db.Tbl_üyeler.Find(p.ÜyeID);
            pers.ÜyeAd = p.ÜyeAd;
            pers.ÜyeSoyad = p.ÜyeSoyad;
            pers.ÜyeMail = p.ÜyeMail;
            pers.KullanıcıAd = p.KullanıcıAd;
            pers.ÜyeSifre = Hashing.CreateHash(p.ÜyeSifre);
            pers.okul = p.okul;
            pers.Tel = p.Tel;
            pers.Foto = p.Foto;
            db.SaveChanges();
            return RedirectToAction("UyeIndex");
        }
        public ActionResult GecmisKitap(int id)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var ktpgcms = db.Tbl_Hareket.Where(x=>x.Üye==id).ToList();
            var uyeAd = db.Tbl_üyeler.Where(x=>x.ÜyeID==id).Select(x=>x.ÜyeAd + " " + x.ÜyeSoyad).FirstOrDefault();
            ViewBag.u1=uyeAd;
            return View(ktpgcms); 

        }
    }
}
