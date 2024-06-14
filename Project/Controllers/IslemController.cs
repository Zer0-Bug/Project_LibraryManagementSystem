using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class IslemController : Controller
    {
        // GET: Islem
        DBKUTUPHANEEntities1 db = new DBKUTUPHANEEntities1();
        public ActionResult IslemIndex()
        {
            if (Session["Role"].ToString() != "admin")
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var degerler = db.Tbl_Hareket.Where(x => x.IslemDurum == true).ToList();
            return View(degerler);
        }
    }
}