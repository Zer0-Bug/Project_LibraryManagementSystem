using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    public class OduncController : Controller
    {
        // GET: Odunc
        DBKUTUPHANEEntities1 db = new DBKUTUPHANEEntities1();
        public ActionResult OIndex()
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var degerler = db.Tbl_Hareket.Where(x => x.IslemDurum == false).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult OduncVer()
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            List<SelectListItem> deger1 = (from x in db.Tbl_üyeler.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.ÜyeAd + " " + x.ÜyeSoyad,
                                               Value = x.ÜyeID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            List<SelectListItem> deger2 = (from x in db.Tbl_kitap.Where(y => y.durum == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KitapAd,
                                               Value = x.KitapID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;
            List<SelectListItem> deger3 = (from x in db.Tbl_personel.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAdSoyad,
                                               Value = x.PersonelID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            return View();
        }
        [HttpPost] // herhangi ekleme işlemi yapılınca bu
        public ActionResult OduncVer(Tbl_Hareket p)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var d1 = db.Tbl_üyeler.Where(x => x.ÜyeID == p.Tbl_üyeler.ÜyeID).FirstOrDefault();
            var d2 = db.Tbl_kitap.Where(x => x.KitapID == p.Tbl_kitap.KitapID).FirstOrDefault();
            var d3 = db.Tbl_personel.Where(x => x.PersonelID == p.Tbl_personel.PersonelID).FirstOrDefault();
            p.Tbl_üyeler = d1;
            p.Tbl_kitap = d2;
            p.Tbl_personel = d3;
            db.Tbl_Hareket.Add(p);
            d2.durum = false;
            db.SaveChanges();
            return RedirectToAction("KitapIndex", "Kitap");
        }
        public ActionResult OduncIade(int id)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var odn = db.Tbl_Hareket.Find(id);
            DateTime d1 = DateTime.Parse(odn.İadeTarih.ToString());
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan d3 = d2 - d1;
            ViewBag.dgr = d3.TotalDays;
            
            return View("OduncIade", odn);
        }
        public ActionResult OduncGuncelle(Tbl_Hareket p)
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var hrk = db.Tbl_Hareket.Find(p.HareketID);
            hrk.UyeGetirmeTarih = p.UyeGetirmeTarih;
            hrk.IslemDurum = true;
            var s2 = db.Tbl_kitap.Where(x => x.KitapID == p.Tbl_kitap.KitapID).FirstOrDefault();
            s2.durum = true;

            //CEZALAR TABLOSUNU DOLDUR
            int punihment_value;
            Tbl_cezalar punishment = new Tbl_cezalar();
            punishment.Hareket = p.HareketID;
            punishment.Üye = p.Üye;
            punishment.BaşlangıcTarih = p.İadeTarih;
            punishment.BitisTarih = p.UyeGetirmeTarih;

            DateTime d1 = DateTime.Parse(punishment.BaşlangıcTarih.ToString());
            DateTime d2 = DateTime.Parse(punishment.BitisTarih.ToString());
            TimeSpan d3 = d2 - d1;
            punihment_value = (int)d3.TotalDays;

            if (punihment_value > 0)
            {
                punishment.Para = punihment_value;
            }
            else
            {
                punishment.Para = 0;
            }
            db.Tbl_cezalar.Add(punishment);
            



            db.SaveChanges();
            
            return RedirectToAction("OIndex");
        }
    }
}