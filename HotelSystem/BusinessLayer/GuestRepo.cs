using HotelSystem.Models;

namespace HotelSystem.BusinessLayer
{
    public class GuestRepo:Repository<Guest,int>
    {
        public GuestRepo(HotelDbContext context):base(context)
        {
        }
    }
}