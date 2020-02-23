using Helper;
using Helper.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelReservationSystem.Controllers
{
    public class GuestController : Controller,ILogger
    {
        public ActionResult GetReservationsForGuest(string Email)
        {
            if (Session["Email"] == null)
            {
                return View("../Error/PresentError", (object)"You Need To Login Again");
            }
            else
            {
                var content = Session["Email"].ToString();
                var rooms = SystemHelper.GenerateRoomsList(SystemHelper.PostAction("GetReservationsForGuest",content, this, true));
                if (rooms==null)
                {
                    rooms = new List<Helper.Models.Room>();
                }
                return View(rooms);
            }
        }
        public void LogErrorToUser(Exception ex)
        {
            RedirectToAction("PresentError", ex);
        }
    }
}