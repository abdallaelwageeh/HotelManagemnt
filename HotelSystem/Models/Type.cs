using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelSystem.Models
{
    public class Type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(200)]
        [Required]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        public int Rate { get; set; }
        [Required]
        public int Level { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}