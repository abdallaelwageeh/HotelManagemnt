using Helper;
using HotelSystem.BusinessLayer;
using HotelSystem.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelSystem.Controllers
{
    public class RoomController : ApiController
    {
        private UnitOfWork context = new UnitOfWork();
        // GET: api/Room
        public string Get()
        {
            string content = JsonConvert.SerializeObject(context.Rooms.GetAllToList());
            return SystemHelper.EncryptContent(content);
        }

        // GET: api/Room/5
        public HttpResponseMessage Get(string requestcontent)
        {
            try
            {
                string content = JsonConvert.SerializeObject(context.Rooms.GetById(int.Parse(SystemHelper.DecryptContent(requestcontent))));
                return Request.CreateResponse(HttpStatusCode.OK, SystemHelper.EncryptContent(content));
            }
            catch (System.Exception)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "I Guess You Send any Data Except Id");
            }
        }

        // POST: api/Room
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Message requestcontent)
        {
            try
            {
                Room room = JsonConvert.DeserializeObject<Room>(SystemHelper.DecryptContent(requestcontent.Value));
                if (context.Rooms.Insert(room))
                {
                    context.SaveChanges();
                    string rooms=SystemHelper.EncryptContent(JsonConvert.SerializeObject(context.Rooms.GetAll()));
                    return Request.CreateResponse(HttpStatusCode.OK, rooms);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "You Send Room Object With Null Data");
                }
            }
            catch (System.Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "You Send Object With Different Type Than Room");
            }
        }

        // PUT: api/Room/5
        public HttpResponseMessage Put([FromBody]Message requestcontent)
        {
            try
            {
                Room room = JsonConvert.DeserializeObject<Room>(SystemHelper.DecryptContent(requestcontent.Value));
                if (context.Rooms.Update(room))
                {
                    context.SaveChanges();
                    string rooms = SystemHelper.EncryptContent(JsonConvert.SerializeObject(context.Rooms.GetAll()));
                    return Request.CreateResponse(HttpStatusCode.OK, rooms);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "You Send Room Object That Dosn't Exist In Database");
                }
            }

            catch (System.Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "You Send Object With Different Type Than Room");
            }
        }

        // DELETE: api/Room/5
        [HttpDelete]
        public HttpResponseMessage Delete([FromBody]Message requestcontent)
        {
            try
            {
                if (context.Rooms.Delete(int.Parse(SystemHelper.DecryptContent(requestcontent.Value))))
                {
                    context.SaveChanges();
                    string rooms = SystemHelper.EncryptContent(JsonConvert.SerializeObject(context.Rooms.GetAll()));
                    return Request.CreateResponse(HttpStatusCode.OK,rooms);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "You Send Room Id That Dosn't Exist In Database");
                }
            }
            catch (System.Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "You Don't Send Data In Good Format");
            }
        }
    }
}
