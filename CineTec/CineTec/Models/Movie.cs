using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineTec.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public string classification_id { get; set; }

        [Required]
        public int director_id { get; set; }

        [Required]
        public string original_name { get; set; }
        public string name { get; set; }

        [Required]
        public string length { get; set; }




    }
}
