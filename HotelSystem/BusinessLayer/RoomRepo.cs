using HotelSystem.Models;
using System.Collections.Generic;

namespace HotelSystem.BusinessLayer
{
    public class RoomRepo:Repository<Room,int>
    {
        private HotelDbContext Context { get; }
        public RoomRepo(HotelDbContext context):base(context)
        {
            Context = context;
        }
    }
}