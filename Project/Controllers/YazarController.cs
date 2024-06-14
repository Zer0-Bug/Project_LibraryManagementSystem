using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class YazarController : Controller
    {
        // GET: Yazar
        DBKUTUPHANEEntities1 db = new DBKUTUPHANEEntities1();
        public ActionResult YazarIndex()
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var degerler = db.Tbl_yazar.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YazarEkle()
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }

            return View();
        }
        public ActionResult YazarEkle(Tbl_yazar yzr)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            if (!ModelState.IsValid)
            {
                return View("YazarEkle");
            }
            db.Tbl_yazar.Add(yzr);
            db.SaveChanges();
            return RedirectToAction("YazarIndex");
        }
        public ActionResult YazarSil(int id)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var yazar = db.Tbl_yazar.Find(id);
            var books = db.Tbl_kitap.Where(x => x.Yazar == id).ToList();
            foreach (var book in books)
            {
                db.Tbl_kitap.Remove(book);
            }

            db.Tbl_yazar.Remove(yazar);
            db.SaveChanges();
            return RedirectToAction("YazarIndex");
        }
        public ActionResult YazarGetir(int id)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var yzr = db.Tbl_yazar.Find(id);
            return View("YazarGetir", yzr);
        }
        public ActionResult YazarGuncelle(Tbl_yazar p)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var yzr = db.Tbl_yazar.Find(p.YazarID);
            yzr.YazarAd = p.YazarAd;
            yzr.YazarSoyad = p.YazarSoyad;
            yzr.Detay = p.Detay;
            db.SaveChanges();
            return RedirectToAction("YazarIndex");
        }
        public ActionResult YazarKitaplar(int id)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var yazar = db.Tbl_kitap.Where(x => x.Yazar == id).ToList();
            var yzrAd = db.Tbl_yazar.Where(x => x.YazarID == id).Select(z=>
            z.YazarAd + " " + z.YazarSoyad).FirstOrDefault();
            ViewBag.y1=yzrAd;
            return View(yazar);
        }
    }
}