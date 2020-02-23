using Helper;
using HotelSystem.BusinessLayer;
using HotelSystem.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Helper.Models;

namespace HotelSystem.Controllers
{
    public class GuestController : ApiController
    {
        private UnitOfWork context = new UnitOfWork();
        [HttpPost]
        [Route("api/Authentication")]
        public HttpResponseMessage Authentication([FromBody]Message requestcontent)
        {
            try
            {
                var user =JsonConvert.DeserializeObject<UserLogin>(SystemHelper.DecryptContent(requestcontent.Value));
                var userInfo = context.Guests.CheckGuest(user);
                if (userInfo!=null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, SystemHelper.EncryptContent(userInfo.FirstName));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
                
            }
            catch (System.Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "I Guess You Send any Data Except Id");
            }
        }
        [HttpPost]
        [Route("api/GetReservationsForGuest")]
        public HttpResponseMessage GetReservationsForGuest([FromBody]Message requestcontent)
        {
            try
            {
                var userInfo = context.Guests.GetByEmail(requestcontent.Value);
                if (userInfo != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, SystemHelper.EncryptContent(JsonConvert.SerializeObject(userInfo.Reservations)));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);
                }

            }
            catch (System.Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "I Guess You Send any Data Except Id");
            }
        }

        // POST: api/Guest
        public HttpResponseMessage Post([FromBody]Message requestcontent)
        {
            try
            {
                Models.Guest guest = JsonConvert.DeserializeObject<Models.Guest>(SystemHelper.DecryptContent(requestcontent.Value));
                if (context.Guests.GetByEmail(guest.Email)==null&&context.Guests.Insert(guest))
                {
                    context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, SystemHelper.EncryptContent("Guest Is Inserted"));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, SystemHelper.EncryptContent("You Send Guest Object With Null Data"));
                }
            }
            catch (System.Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }
        }

        // PUT: api/Guest/5
        public HttpResponseMessage Put([FromBody]string requestcontent)
        {
            try
            {
                Models.Guest guest = JsonConvert.DeserializeObject<Models.Guest>(SystemHelper.DecryptContent(requestcontent));
                if (context.Guests.Update(guest))
                {
                    context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Guest Is Updated");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "You Send Guest Object That Dosn't Exist In Database");
                }
            }

            catch (System.Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "You Send Object With Different Type Than Guest");
            }
        }

        // DELETE: api/Guest/5
        public HttpResponseMessage Delete(string requestcontent)
        {
            try
            {
                if (context.Guests.Delete(int.Parse(SystemHelper.DecryptContent(requestcontent))))
                {
                    context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Guest Is Deleted");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "You Send Guest Id That Dosn't Exist In Database");
                }
            }
            catch (System.Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "You Don't Send Data In Good Format");
            }
        }
    }
}
