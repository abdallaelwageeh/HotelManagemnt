using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelSystem.Models
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(10)]
        [Required]
        public string Number { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public int TypeId { get; set; }
        public string ImagePath { get; set; }
        public int?GuestId { get; set; }
        [Required]
        [DefaultValue(false)]
        public bool IsReserved { get; set; }
    }
}