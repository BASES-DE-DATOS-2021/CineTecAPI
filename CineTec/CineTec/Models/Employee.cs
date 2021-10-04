using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineTec.Models
{
    public class Employee
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string first_surname { get; set; }
        public string second_surname { get; set; }
        public DateTime birth_date { get; set; }
        public string phone_number { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int age { get; set; }
        [ForeignKey("Branch")]
        public string branch_id { get; set; }
    }
}
 