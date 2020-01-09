using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokeBuilder_Auxilium.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error404
        public ActionResult Error404()
        {
            return View();
        }

        public ActionResult Error400()
        {
            return View();
        }
    }
}