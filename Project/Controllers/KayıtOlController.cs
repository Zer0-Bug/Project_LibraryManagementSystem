using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using FrontCode.Libraries;
using System.Text.RegularExpressions;

namespace MvcKutuphane.Controllers
{
    public class KayıtOlController : Controller
    {
        // GET: KayıtOl
        DBKUTUPHANEEntities1 db = new DBKUTUPHANEEntities1();

        [HttpGet]
        [AllowAnonymous] // Bu sayfayı anonim kullanıcıların erişebilmesi için
        public ActionResult Kayıt()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Kayıt(Tbl_üyeler kayıt)
        {
            if (!ModelState.IsValid)
            {
                return View("Kayıt");
            }

            // Email, Kullanıcı Adı ve Telefon benzersizliğini kontrol etme
            bool isEmailExist = db.Tbl_üyeler.Any(x => x.ÜyeMail == kayıt.ÜyeMail);
            bool isUsernameExist = db.Tbl_üyeler.Any(x => x.KullanıcıAd == kayıt.KullanıcıAd);
            bool isPhoneExist = db.Tbl_üyeler.Any(x => x.Tel == kayıt.Tel);

            if (isEmailExist || isUsernameExist || isPhoneExist)
            {
                if (isEmailExist)
                {
                    ModelState.AddModelError("ÜyeMail", "Bu email zaten kayıtlı.");
                }
                string phonePattern = @"^0\d{10}$"; // Telefon numarasının 0 ile başlayıp 10 haneli olması kontrolü
                if (!Regex.IsMatch(kayıt.Tel, phonePattern))
                {
                    ModelState.AddModelError("Tel", "Telefon numarası geçerli değil. Türkiye formatında 0 ile başlayan ve 10 haneli olmalıdır.");
                    return View("Kayıt");
                }
                if (isUsernameExist)
                {
                    ModelState.AddModelError("KullanıcıAd", "Bu kullanıcı adı zaten kullanılıyor.");
                }
                if (isPhoneExist)
                {
                    ModelState.AddModelError("Tel", "Bu telefon numarası zaten kayıtlı.");
                }

                return View("Kayıt", kayıt);
            }

            
            var uyepssw = Hashing.CreateHash(kayıt.ÜyeSifre);
            kayıt.ÜyeSifre = uyepssw;

            
            db.Tbl_üyeler.Add(kayıt);
            db.SaveChanges();

            return RedirectToAction("LoginIndex", "Login");
        }

    }
}