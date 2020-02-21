using Helper;
using HotelSystem.BusinessLayer;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using HotelSystem.Models;

namespace HotelSystem.Controllers
{
    public class TypeController : ApiController
    {
        private UnitOfWork context = new UnitOfWork();
        // GET: api/Type
        public string Get()
        {
            string content = JsonConvert.SerializeObject(context.Types.GetAllToList());
            return SystemHelper.EncryptContent(content);
        }

        // GET: api/Type/5
        public HttpResponseMessage Get(string requestcontent)
        {
            try
            {
                string content = JsonConvert.SerializeObject(context.Types.GetById(int.Parse(SystemHelper.DecryptContent(requestcontent))));
                return Request.CreateResponse(HttpStatusCode.OK, SystemHelper.EncryptContent(content));
            }
            catch (System.Exception)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "I Guess You Send any Data Except Id");
            }
        }

        // POST: api/Type
        [HttpPost]
        public HttpResponseMessage Post([FromBody]string requestcontent)
        {
            try
            {
                Type type = JsonConvert.DeserializeObject<Type>(SystemHelper.DecryptContent(requestcontent));
                if (context.Types.Insert(type))
                {
                    context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Type Is Inserted");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "You Send Type Object With Null Data");
                }
            }
            catch (System.Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "You Send Object With Different Type Than Type");
            }
        }

        // PUT: api/Type/5
        public HttpResponseMessage Put([FromBody]string requestcontent)
        {
            try
            {
                Type type = JsonConvert.DeserializeObject<Type>(SystemHelper.DecryptContent(requestcontent));
                if (context.Types.Update(type))
                {
                    context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Type Is Updated");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "You Send Type Object That Dosn't Exist In Database");
                }
            }

            catch (System.Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "You Send Object With Different Type Than Type");
            }
        }

        // DELETE: api/Type/5
        public HttpResponseMessage Delete(string requestcontent)
        {
            try
            {
                if (context.Types.Delete(int.Parse(SystemHelper.DecryptContent(requestcontent))))
                {
                    context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Type Is Deleted");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "You Send Type Id That Dosn't Exist In Database");
                }
            }
            catch (System.Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "You Don't Send Data In Good Format");
            }
        }
    }
}
