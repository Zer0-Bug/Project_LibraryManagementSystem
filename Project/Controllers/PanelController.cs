using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FrontCode.Libraries;

namespace MvcKutuphane.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {
        DBKUTUPHANEEntities1 db = new DBKUTUPHANEEntities1();
       
        [HttpGet]

        public ActionResult PanelIndex()
        {
            var uyemail = (string)Session["mail"];
            //var degerler = db.Tbl_üyeler.FirstOrDefault(z => z.ÜyeMail==uyemail);
            var degerler = db.Tbl_duyurular.ToList();
            var d1 = db.Tbl_üyeler.Where(x=>x.ÜyeMail==uyemail).Select(y=>y.ÜyeAd).FirstOrDefault();
            ViewBag.D1 = d1;
            var d2 = db.Tbl_üyeler.Where(x => x.ÜyeMail == uyemail).Select(y => y.ÜyeSoyad).FirstOrDefault();
            ViewBag.D2 = d2;
            var d3 = db.Tbl_üyeler.Where(x => x.ÜyeMail == uyemail).Select(y => y.okul).FirstOrDefault();
            ViewBag.D3 = d3;
            var d4 = db.Tbl_üyeler.Where(x => x.ÜyeMail == uyemail).Select(y => y.Tel).FirstOrDefault();
            ViewBag.D4 = d4;
            var uyeId = db.Tbl_üyeler.Where(x => x.ÜyeMail == uyemail).Select(y => y.ÜyeID).FirstOrDefault();
            var d5 = db.Tbl_Hareket.Where(x=>x.Üye==uyeId).Count();
            ViewBag.D5 = d5;
            var d6 = db.Tbl_mesajlar.Where(x => x.Alıcı == uyemail).Count();
            ViewBag.D6 = d6;
            var d7 = db.Tbl_duyurular.Count();
            ViewBag.D7 = d7;
            return View(degerler);
        }
        [HttpPost]
        public ActionResult PanelIndex2(Tbl_üyeler p)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("PanelIndex");
            }
            var kullanıcı = (string)Session["mail"];
            var uye = db.Tbl_üyeler.FirstOrDefault(x => x.ÜyeMail == kullanıcı);
            uye.ÜyeSifre = Hashing.CreateHash(p.ÜyeSifre);
            uye.ÜyeAd = p.ÜyeAd;
            uye.ÜyeSoyad = p.ÜyeSoyad;
            uye.KullanıcıAd = p.KullanıcıAd;
            uye.Foto = p.Foto;
            uye.okul=p.okul;
            
            db.SaveChanges();
            return RedirectToAction("PanelIndex");
        }
        public ActionResult Kitaplarim()
        {
            var kullanıcı = (string)Session["mail"];
            var id = db.Tbl_üyeler.Where(x=>x.ÜyeMail==kullanıcı.ToString()).Select(z=>z.ÜyeID).FirstOrDefault();
            var degerler = db.Tbl_Hareket.Where(x => x.Üye == id).ToList();
            return View(degerler);
        }
        public ActionResult duyurular()
        {
            var duyuru = db.Tbl_duyurular.ToList();
            return View(duyuru);
        }
        public ActionResult cıkıs()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginIndex", "Login");
        }
        public PartialViewResult part1()
        {
            return PartialView();
        }
        public PartialViewResult part2()
        {
            var kullanıcı = (string)Session["mail"];
            var id = db.Tbl_üyeler.Where(x => x.ÜyeMail == kullanıcı).Select(y => y.ÜyeID).FirstOrDefault();
            var uyebul = db.Tbl_üyeler.Find(id);
            return PartialView("part2" ,uyebul);
        }
    }
}
