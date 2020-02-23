using Helper;
using Helper.Interfaces;
using Helper.Models;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace HotelReservationSystem.Controllers
{
    public class RoomController : Controller,ILogger
    {
        
        [CustomAuthorize]
        public ActionResult Book( Room room)
        {
            if (Session["Email"]==null)
            {
                return View("../Error/PresentError",(object)"You Need To Login Again");
            }
            else
            {
                var content = SystemHelper.EncryptContent(JsonConvert.SerializeObject(new Reservation { Room = room, UserName = Session["Email"].ToString() }));
                var rooms = SystemHelper.GenerateRoomsList(SystemHelper.PostAction("ReserveRoom", content, null, true));
                return View("../Guest/GetReservationsForGuest", rooms);
            }
        }
        public void LogErrorToUser(Exception ex)
        {
            RedirectToAction("PresentError", ex);
        }
        
    }
}
