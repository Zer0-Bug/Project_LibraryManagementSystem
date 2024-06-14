using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using MvcKutuphane.Models.Sınıflarım;
namespace MvcKutuphane.Controllers
{
    public class VitrinController : Controller
    {
        // GET: Vitrin
        DBKUTUPHANEEntities1 db = new DBKUTUPHANEEntities1();
        [HttpGet]
        public ActionResult VitrinIndex()
        {
            Session["Role"] = "";
            Class1 cs = new Class1();
            cs.Deger1 = db.Tbl_kitap.ToList();
            cs.Deger2 = db.Tbl_hakkımızda.ToList();
            //var degerler = db.Tbl_kitap.ToList();
            return View(cs);
        }
        [HttpPost]
        public ActionResult VitrinIndex(Tbl_iletisim t)
        {
            db.Tbl_iletisim.Add(t);
            db.SaveChanges();
            return RedirectToAction("VitrinIndex");
        }
    }
}