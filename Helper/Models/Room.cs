using System.ComponentModel;

namespace Helper.Models
{
    public class Room
    { 
        public int Id { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string Price { get; set; }
        public int TypeId { get; set; }
        public string ImagePath { get; set; }
        public int?GuestId { get; set; }
        public bool IsReserved { get; set; }
    }
}