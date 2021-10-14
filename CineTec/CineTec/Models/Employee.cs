using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineTec.Models
{
    public class Employee
    {
        [Key]
        public int cedula { get; set; }

        [Required]
        public string first_name { get; set; }

        public string middle_name { get; set; }

        [Required]
        public string first_surname { get; set; }

        public string second_surname { get; set; }

        [Required]
        public DateTime birth_date { get; set; }

        [Required]
        public DateTime start_date { get; set; }
        public string phone_number { get; set; }

        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }

        [ForeignKey("Branch")]
        public string branch_id { get; set; }

        public string FormattedBirth_date
        {
            get
            {
                return string.Format("{0:dd/MM/yy}", birth_date);
            }
        }

        public string FormattedStart_date
        {
            get
            {
                return string.Format("{0:dd/MM/yy}", start_date);
            }
        }
    }
}
 