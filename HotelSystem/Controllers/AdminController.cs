using Helper;
using HotelSystem.BusinessLayer;
using HotelSystem.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelSystem.Controllers
{
    public class AdminController : ApiController
    {
        private UnitOfWork context = new UnitOfWork();
        // GET: api/Admin
        public string Get()
        {
            string content = JsonConvert.SerializeObject(context.Admins.GetAllToList());
            return SystemHelper.EncryptContent(content);
        }

        // GET: api/Admin/5
        public HttpResponseMessage Get(string requestcontent)
        {
            try
            {
                string content = JsonConvert.SerializeObject(context.Admins.GetById(int.Parse(SystemHelper.DecryptContent(requestcontent))));
                return Request.CreateResponse(HttpStatusCode.OK, SystemHelper.EncryptContent(content));
            }
            catch (System.Exception)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "I Guess You Send any Data Except Id");
            }
        }

        // POST: api/Admin
        public HttpResponseMessage Post([FromBody]string requestcontent)
        {
            try
            {
                Admin admin = JsonConvert.DeserializeObject<Admin>(SystemHelper.DecryptContent(requestcontent));
                if (context.Admins.Insert(admin))
                {
                    context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Admin Is Inserted");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "You Send Admin Object With Null Data");
                }
            }
            catch (System.Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "You Send Object With Different Type Than Admin");
            }
        }

        // PUT: api/Admin/5
        public HttpResponseMessage Put([FromBody]string requestcontent)
        {
            try
            {
                Admin admin = JsonConvert.DeserializeObject<Admin>(SystemHelper.DecryptContent(requestcontent));
                if (context.Admins.Update(admin))
                {
                    context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Admin Is Updated");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "You Send Admin Object That Dosn't Exist In Database");
                }
            }

            catch (System.Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "You Send Object With Different Type Than Admin");
            }
        }

        // DELETE: api/Admin/5
        public HttpResponseMessage Delete(string requestcontent)
        {
            try
            {
                if (context.Admins.Delete(int.Parse(SystemHelper.DecryptContent(requestcontent))))
                {
                    context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Admin Is Deleted");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "You Send Admin Id That Dosn't Exist In Database");
                }
            }
            catch (System.Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "You Don't Send Data In Good Format");
            }
        }
    }
}
