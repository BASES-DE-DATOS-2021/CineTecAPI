using System.ComponentModel.DataAnnotations;

namespace CineTec.Models
{
    public class Classification
    {
        [Key]
        public string code { get; set; }
        public string details { get; set; }
        public int age_rating { get; set; }
    }
}
