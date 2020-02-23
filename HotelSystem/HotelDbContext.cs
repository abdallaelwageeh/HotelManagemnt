using HotelSystem;
using HotelSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Type = HotelSystem.Models.Type;

namespace HotelSystem
{
    public class HotelDbContext:DbContext
    {
        public HotelDbContext():base("name=HotelDbConnection")
        {
          Database.SetInitializer(new HotelDBInitializer());
        }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<Models.Type> Types { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
    }
}

public class HotelDBInitializer : DropCreateDatabaseAlways<HotelDbContext>
{
    protected override void Seed(HotelDbContext context)
    {
        IList<HotelSystem.Models.Type> defaultTypes = new List<Type>();
        defaultTypes.Add(new HotelSystem.Models.Type() {Id=1, Name = "Sweet", Rate = 5, Level = 5, Description = "" });
        defaultTypes.Add(new HotelSystem.Models.Type() { Id = 2, Name = "BigRoom", Rate = 3, Level = 4, Description = "" });
        defaultTypes.Add(new HotelSystem.Models.Type() { Id = 3, Name = "SmallRoom", Rate = 1, Level = 2, Description = "" });
        context.Types.AddRange(defaultTypes);

        IList<HotelSystem.Models.Room> defaultRooms = new List<Room>();
        defaultRooms.Add(new HotelSystem.Models.Room() { Number = "1", Capacity = 5, Price = "5000", Description = "IT IS AMAZING APARTMENT", TypeId = 1 });
        defaultRooms.Add(new HotelSystem.Models.Room() { Number = "2", Capacity = 3, Price = "3000", Description = "HELLO FOR YOUR HOME !!", TypeId = 2,IsReserved=true});
        defaultRooms.Add(new HotelSystem.Models.Room() { Number = "3", Capacity = 1, Price = "1550", Description = "WELCOME TO OUR SYSTEM", TypeId=3});
        context.Rooms.AddRange(defaultRooms);
        context.Guests.Add(new Guest {FirstName="Abdaullah",MiddleName="Mahmoud",Email="AbdallaElwageeh@yahoo.com",Mobile="01067926393",Password="Abdalla"});
        base.Seed(context);
    }
}