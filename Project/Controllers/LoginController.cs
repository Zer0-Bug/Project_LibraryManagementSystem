using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using System.Web.Security;
using FrontCode.Libraries;

namespace MvcKutuphane.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        DBKUTUPHANEEntities1 db = new DBKUTUPHANEEntities1();
        public ActionResult LoginIndex()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginIndex(Tbl_üyeler p)
        {
            var encoded_hash = Hashing.VerifyPassword(p.ÜyeSifre);
            var bilgiler = db.Tbl_üyeler.FirstOrDefault(x => x.ÜyeMail == p.ÜyeMail && x.ÜyeSifre == encoded_hash);
            
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.ÜyeMail, false);
                Session["mail"]=bilgiler.ÜyeMail.ToString();
                Session["username"] = bilgiler.ÜyeAd;
                Session["Role"] = "member";
                //TempData["ad"] = bilgiler.ÜyeAd.ToString();
               // TempData["soyad"] = bilgiler.ÜyeSoyad.ToString();
                //TempData["kulAd"] = bilgiler.KullanıcıAd.ToString();
                //TempData["sifre"] = bilgiler.ÜyeSifre.ToString();
                //TempData["okul"] = bilgiler.okul.ToString();
                //TempData["id"] = bilgiler.ÜyeID.ToString();
                return RedirectToAction("PanelIndex", "Panel");
            }
            return View();
        }
        
    }
}