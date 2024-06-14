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
    public class AdminController : Controller
    {
        DBKUTUPHANEEntities1 db = new DBKUTUPHANEEntities1();
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Tbl_personel p)
        {
            var encoded_hash = Hashing.VerifyPassword(p.PersonelPassword);
            var personel = db.Tbl_personel.FirstOrDefault(x => x.PersonelEmail == p.PersonelEmail && x.PersonelPassword == encoded_hash);

            if (personel != null)
            {
                FormsAuthentication.SetAuthCookie(personel.PersonelEmail, false);
                Session["mail"] = personel.PersonelEmail.ToString();
                Session["Role"] = "admin";
                return RedirectToAction("PersonelIndex", "Personel");
            }

            return View();
        }

    }
}