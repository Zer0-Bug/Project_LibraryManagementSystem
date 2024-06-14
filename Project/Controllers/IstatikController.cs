using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class IstatikController : Controller
    {
        // GET: Istatik
        DBKUTUPHANEEntities1 db = new DBKUTUPHANEEntities1();
        public ActionResult IstatikIndex()
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var dgr1 = db.Tbl_üyeler.Count();
            var dgr2 = db.Tbl_kitap.Count();
            var dgr3 = db.Tbl_kitap.Where(x=>x.durum==false).Count();
            var dgr4 = db.Tbl_cezalar.Sum(x => x.Para);
            ViewBag.deger1 = dgr1;
            ViewBag.deger2 = dgr2;
            ViewBag.deger3= dgr3;
            ViewBag.deger4= dgr4;
            return View();
        }
        public ActionResult hava()
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            return View();
        }
      public ActionResult Galeri()
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            return View();
        }
        public ActionResult resimYukle(HttpPostedFileBase dosya)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            if (dosya.ContentLength > 0)
            {
                string dosyayolu = Path.Combine(Server.MapPath("~/web2/resimler/"),Path.GetFileName(dosya.FileName));
                dosya.SaveAs(dosyayolu);
            }
            return RedirectToAction("Galeri");
        }
        public ActionResult LinqKart()
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var deger1 = db.Tbl_kitap.Count();
            var deger2 = db.Tbl_üyeler.Count();
            var deger3 = db.Tbl_cezalar.Sum(x=>x.Para);
            var deger4 = db.Tbl_kitap.Where(x=>x.durum==false).Count();
            var deger5 = db.Tbl_kategori.Count();

            // var deger8 = db.ENFAZLAKİTAPYAZANYAZAR().FirstOrDefault();
            var deger9 = db.Tbl_kitap.GroupBy(x=>x.Yayınevi).OrderByDescending(z=>z.Count()).Select(y=>
            new {y.Key}).FirstOrDefault();

            var deger11 = db.Tbl_iletisim.Count();
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            ViewBag.dgr4 = deger4;
            ViewBag.dgr5 = deger5;

            //ViewBag.dgr8 = deger8;
            ViewBag.dgr9 = deger9;

            ViewBag.dgr11 = deger11;

            var d = (from a in db.Tbl_yazar join b in db.Tbl_kitap on a.YazarID equals b.Yazar select new
            {
                a.YazarAd,
                a.YazarSoyad,
                b.Yazar
            }).ToList().GroupBy(c=> new {c.YazarAd,c.YazarSoyad}).OrderByDescending(c=>c.Count()).FirstOrDefault();
            return View();
        } 
    }
}