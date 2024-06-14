using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class duyuruController : Controller
    {
        // GET: duyuru
        DBKUTUPHANEEntities1 db = new DBKUTUPHANEEntities1();
        public ActionResult duyuruIndex()
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var degerler = db.Tbl_duyurular.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult yeniDuyuru()
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            return View();
        }
        [HttpPost]
        public ActionResult yeniDuyuru(Tbl_duyurular t)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            db.Tbl_duyurular.Add(t);
            db.SaveChanges();
            return RedirectToAction("duyuruIndex");
        }
        public ActionResult duyuruSil(int id)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var d = db.Tbl_duyurular.Find(id);
            db.Tbl_duyurular.Remove(d);
            db.SaveChanges();
            return RedirectToAction("duyuruIndex");
        }
        public ActionResult duyuruDetay(Tbl_duyurular d)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var duyuru = db.Tbl_duyurular.Find(d.ID);

            return View("duyuruDetay", duyuru);
        }
        public ActionResult duyuruGuncelle(Tbl_duyurular tbl)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var duy = db.Tbl_duyurular.Find(tbl.ID);
            duy.kategori = tbl.kategori;
            duy.icerik = tbl.icerik;
            duy.tarih = tbl.tarih;
            db.SaveChanges();
            return RedirectToAction("duyuruIndex");
        }
    }
}