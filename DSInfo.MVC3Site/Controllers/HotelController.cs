using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSInfo.MVC3Site.Utility;

namespace DSInfo.MVC3Site.Controllers
{
    public class HotelController : Controller
    {
        //
        // GET: /Hotel/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult test()
        {

            CorpHotelSearch.getRecHotelList();
            return View();
        }

    }
}
