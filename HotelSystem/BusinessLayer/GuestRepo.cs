using HotelSystem.Models;

using Helper.Models;
using System.Linq;

namespace HotelSystem.BusinessLayer
{
    public class GuestRepo:Repository<Models.Guest, int>
    {
        private HotelDbContext Context { get;}
        public GuestRepo(HotelDbContext context):base(context)
        {
            Context = context;
        }
        public Models.Guest CheckGuest(UserLogin guets)
        {
            try
            {
                return Context.Guests.Where(x => x.Email.Equals(guets.UserName) && x.Password.Equals(guets.Password)).FirstOrDefault(); 
            }
            catch (System.Exception e)
            {
                return null;
            }
        }
        public Models.Guest GetByEmail(string Email)
        {
            try
            {
                return Context.Guests.Where(x => x.Email.Equals(Email)).FirstOrDefault();
            }
            catch (System.Exception e)
            {
                return null;
            }
        }
    }
}