using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineTec.Models
{
    public class Projection
    {
        [ForeignKey("Room")]
        public int room_id { get; set; }
        [ForeignKey("Movie")]
        public int movie_id { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string date { get; set; }
        public string  time { get; set; }



    }
}
