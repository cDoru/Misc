using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security;
using System.Web.Mvc;

namespace AngularPractice.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Http.HttpPost]
        public JsonResult Post(Dto content)
        {
            return Json(content);
        }

        public JsonResult Get()
        {
            return Json(string.Empty);
        }

    }

    public class Dto
    {
        public string Text { get; set; }
    }
}
