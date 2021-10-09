using System.ComponentModel.DataAnnotations;

namespace CineTec.Models
{
    public class Classification
    {
        [Key]
        public string code { get; set; }

        [Required]
        public string details { get; set; }

        [Required]
        public int age_rating { get; set; }
    }
}
