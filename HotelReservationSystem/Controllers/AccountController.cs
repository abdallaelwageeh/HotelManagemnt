using Helper;
using Helper.Interfaces;
using Helper.Models;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace HotelReservationSystem.Controllers
{
    public class AccountController : Controller,ILogger
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult ApiRegister(Guest guest)
        {
            if (ModelState.IsValid)
            {
                var content = SystemHelper.EncryptContent(JsonConvert.SerializeObject(guest));
                if (SystemHelper.PostAction("Guest", content, this, true) != "")
                {
                    Session.Add("UserName", guest.FirstName);
                    Session.Add("Email", guest.Email);
                    FormsAuthentication.SetAuthCookie(guest.FirstName, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("Register");
        }
        public ActionResult ApiLogin(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                var content = SystemHelper.EncryptContent(JsonConvert.SerializeObject(user));
                string userName = SystemHelper.PostAction("Authentication", content,this,true);
                if (userName != "")
                {
                    Session.Add("UserName", userName);
                    Session.Add("Email", user.UserName);
                    FormsAuthentication.SetAuthCookie(userName, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "invalid Username or Password");
                    return View("Login");
                }
            }
            return View("Login");
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public void LogErrorToUser(Exception ex)
        {
            RedirectToAction("PresentError", ex);
        }
    }
}