using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelSystem.Models
{
    public class Guest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string FirstName { get; set; }
        [StringLength(200)]
        public string MiddleName { get; set; }
        [StringLength(200)]
        public string LastName { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        [Required]
        [StringLength(10)]
        public string Mobile { get; set; }
        [StringLength(14)]
        public string NationalityId { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public ICollection<Room> Reservations { get; set; }
    }
}