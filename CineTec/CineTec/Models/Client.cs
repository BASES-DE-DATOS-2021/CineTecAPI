using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineTec.Models
{
    public class Client
    {
        [Key]
        public int cedula { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string first_surname { get; set; }
        public string second_surname { get; set; }
        public DateTime birth_date { get; set; }
        public string phone_number { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int age { get; set; }
    }
}
 