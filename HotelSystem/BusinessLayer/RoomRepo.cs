using HotelSystem.Models;

namespace HotelSystem.BusinessLayer
{
    public class RoomRepo:Repository<Room,int>
    {
        public RoomRepo(HotelDbContext context):base(context)
        {
        }
    }
}