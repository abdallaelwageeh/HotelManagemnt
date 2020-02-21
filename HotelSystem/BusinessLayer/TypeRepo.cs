using HotelSystem.Models;

namespace HotelSystem.BusinessLayer
{
    public class TypeRepo:Repository<Type,int>
    {
        public TypeRepo(HotelDbContext context):base(context)
        {
        }
    }
}