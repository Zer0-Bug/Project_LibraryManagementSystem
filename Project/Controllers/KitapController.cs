using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class KitapController : Controller
    {
        // GET: Kitap
        DBKUTUPHANEEntities1 db = new DBKUTUPHANEEntities1();
        public ActionResult KitapIndex(string p)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var kitaplar = from k in db.Tbl_kitap select k;
            if(!string.IsNullOrEmpty(p))
            {
                kitaplar = kitaplar.Where(m=>m.KitapAd.Contains(p));
            }
           // var kitaplar = db.Tbl_kitap.ToList();
            return View(kitaplar.ToList());
        }
        [HttpGet]
        public ActionResult KitapEkle() 
        {//dropdown ile kullanılır il secerken mesela kaydırmalı yer yapmak için
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            List<SelectListItem> deger1 = (from i in db.Tbl_kategori.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Ad,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            List<SelectListItem> deger2 = (from i in db.Tbl_yazar.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.YazarAd + ' ' + i.YazarSoyad,
                                               Value = i.YazarID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;
            return View();// sayfalar arasında veri taşıma işlemi yapar
        }
        [HttpPost]
        public ActionResult KitapEkle(Tbl_kitap p)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var ktg=db.Tbl_kategori.Where(k => k.ID == p.Tbl_kategori.ID).FirstOrDefault();
            var yzr=db.Tbl_yazar.Where(y => y.YazarID == p.Tbl_yazar.YazarID).FirstOrDefault();
            p.Tbl_kategori = ktg;
            p.Tbl_yazar = yzr;
            db.Tbl_kitap.Add(p);
            db.SaveChanges();
            return RedirectToAction("KitapIndex");
        }
        public ActionResult KitapSil(int id)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var kitap = db.Tbl_kitap.Find(id);
            db.Tbl_kitap.Remove(kitap);
            db.SaveChanges();
            return RedirectToAction("KitapIndex");
        }
        public ActionResult KitapGetir(int id)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var ktp = db.Tbl_kitap.Find(id);
            List<SelectListItem> deger1 = (from i in db.Tbl_kategori.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Ad,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            List<SelectListItem> deger2 = (from i in db.Tbl_yazar.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.YazarAd + ' ' + i.YazarSoyad,
                                               Value = i.YazarID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;
            return View("KitapGetir", ktp);
        }
        public ActionResult KitapGuncelle(Tbl_kitap p)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var kitap = db.Tbl_kitap.Find(p.KitapID);
            kitap.KitapAd = p.KitapAd; // KİTAPIN ADI P DEN GELEN YANİ YENİ GİRDİĞİMİZLE DEĞİŞSİN
            kitap.BasımYıl = p.BasımYıl;
            kitap.SayfaSayısı = p.SayfaSayısı;
            kitap.Yayınevi = p.Yayınevi;
            kitap.durum = true;
            var ktg = db.Tbl_kategori.Where(k=>k.ID == p.Tbl_kategori.ID).FirstOrDefault();
            var yzr = db.Tbl_yazar.Where(y=>y.YazarID==p.Tbl_yazar.YazarID).FirstOrDefault();
            kitap.Kategori = ktg.ID;
            kitap.Yazar = yzr.YazarID;
            db.SaveChanges();
            return RedirectToAction("KitapIndex");
        }
    }
}