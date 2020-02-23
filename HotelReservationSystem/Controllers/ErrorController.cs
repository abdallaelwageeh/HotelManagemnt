using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelReservationSystem.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult PresentError(string Error)
        {
            return View(Error);
        }
    }
}