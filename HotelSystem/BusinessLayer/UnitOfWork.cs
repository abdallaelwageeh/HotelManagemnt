using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelSystem.BusinessLayer
{
    public class UnitOfWork
    {
        private HotelDbContext Context = new HotelDbContext();
        public RoomRepo Rooms 
        {
            get { return new RoomRepo(Context); }
        }
        public GuestRepo Guests
        {
            get { return new GuestRepo(Context); }
        }
        public AdminRepo Admins
        {
            get { return new AdminRepo(Context); }
        }
        public TypeRepo Types
        {
            get { return new TypeRepo(Context); }
        }
        public int SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}