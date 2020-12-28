using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetProject.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name  { get; set; }
        [Required]
        public string DOB { get; set; }
        [Required]
        [EmailAddress]
       
        public string Email { get; set; }
        [Required]
        [StringLength(10,MinimumLength =10)]
        [Phone]
        public string Phone { get; set; }
        [Required]
        public string City { get; set; }
    }
}
