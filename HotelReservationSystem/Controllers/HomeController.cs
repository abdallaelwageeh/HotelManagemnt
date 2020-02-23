using Helper;
using Helper.Interfaces;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace HotelReservationSystem.Controllers
{
    public class HomeController : Controller,ILogger
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Search()
        {
            var rooms=Task.Factory.StartNew(() => SystemHelper.GenerateRoomsList(SystemHelper.GetAction("Room", true,this))).GetAwaiter().GetResult();
            ViewBag.Message = "Your Search page.";
            return View(rooms);

        }
        public void LogErrorToUser(Exception ex)
        {
            RedirectToAction("PresentError", ex);
        }
    }
}