using Helper;
using HotelSystem.BusinessLayer;
using HotelSystem.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelSystem.Controllers
{
    public class GuestController : ApiController
    {
        private UnitOfWork context = new UnitOfWork();
        // GET: api/Guest
        public string Get()
        {
            string content = JsonConvert.SerializeObject(context.Guests.GetAllToList());
            return SystemHelper.EncryptContent(content);
        }

        // GET: api/Guest/5
        public HttpResponseMessage Get(string requestcontent)
        {
            try
            {
                string content = JsonConvert.SerializeObject(context.Guests.GetById(int.Parse(SystemHelper.DecryptContent(requestcontent))));
                return Request.CreateResponse(HttpStatusCode.OK, SystemHelper.EncryptContent(content));
            }
            catch (System.Exception)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "I Guess You Send any Data Except Id");
            }
        }

        // POST: api/Guest
        public HttpResponseMessage Post([FromBody]string requestcontent)
        {
            try
            {
                Guest guest = JsonConvert.DeserializeObject<Guest>(SystemHelper.DecryptContent(requestcontent));
                if (context.Guests.Insert(guest))
                {
                    context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Guest Is Inserted");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "You Send Guest Object With Null Data");
                }
            }
            catch (System.Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "You Send Object With Different Type Than Guest");
            }
        }

        // PUT: api/Guest/5
        public HttpResponseMessage Put([FromBody]string requestcontent)
        {
            try
            {
                Guest guest = JsonConvert.DeserializeObject<Guest>(SystemHelper.DecryptContent(requestcontent));
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
