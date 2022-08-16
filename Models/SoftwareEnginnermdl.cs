using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoreAPIIntigration.Models
{
    public class SoftwareEnginnermdl
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Phone { get; set; }
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        public String Address { get; set; }
        [Required]
        public String Salary { get; set; }
        public String Designation { get; set; }
    }
}
