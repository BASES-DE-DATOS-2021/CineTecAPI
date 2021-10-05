using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineTec.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string classification_id { get; set; }
        public int director_id { get; set; }

        public string image { get; set; }
        public string original_name { get; set; }
        public string name { get; set; }
        public string length { get; set; }




    }
}
