using HotelSystem.Models;

namespace HotelSystem.BusinessLayer
{
    public class AdminRepo:Repository<Admin,int>
    {
        public AdminRepo(HotelDbContext context):base(context)
        {
        }
    }
}