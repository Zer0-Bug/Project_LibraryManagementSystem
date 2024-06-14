using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    public class ForbiddenController : Controller
    {
        // GET: Forbidden
        public ActionResult Index()
        {
            return View();
        }
    }
}