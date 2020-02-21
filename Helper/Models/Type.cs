using System.Collections.Generic;

namespace Helper.Models
{
    public class Type
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
        public int Level { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}