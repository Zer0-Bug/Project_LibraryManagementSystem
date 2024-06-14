using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class KategoriController : Controller
    {
        DBKUTUPHANEEntities1 db = new DBKUTUPHANEEntities1();
        public ActionResult Index()
        {
            if(Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index","Forbidden");
            }
            var degerler = db.Tbl_kategori.Where(x=>x.Durum==true).ToList();
            return View(degerler);
        }
        [HttpGet] //sayfa yüklemek istediğimiz zaman bu başka işlem yapmıyosak
        public ActionResult KategoriEkle() {
        if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }

            return View();
        }
        [HttpPost] // herhangi ekleme işlemi yapılınca bu
        public ActionResult KategoriEkle(Tbl_kategori p) {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }

            p.Durum = true;
            db.Tbl_kategori.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var kategori=db.Tbl_kategori.Find(id);
            var books = db.Tbl_kitap.Where(x => x.Kategori == id).ToList();
            foreach (var book in books)
            {
                db.Tbl_kitap.Remove(book);
            }
            db.Tbl_kategori.Remove(kategori);
            //kategori.Durum = false;
            db.SaveChanges();
            
            return RedirectToAction("Index"); //beni şu aksiyona yönlendir
        }
        public ActionResult KategoriGetir(int id)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var ktg = db.Tbl_kategori.Find(id);
            return View("KategoriGetir",ktg); //kategori getir sayfasını döndür ama ktg bilgilerini içeren sayfa döndür

        }
        public ActionResult KategoriGuncelle(Tbl_kategori p) 
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var ktg = db.Tbl_kategori.Find(p.ID);
            ktg.Ad = p.Ad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}