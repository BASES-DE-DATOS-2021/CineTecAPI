using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

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


        [Required]
        [Column(TypeName = "Date")]
        public DateTime date { get; set; }

        [Required]
        public string schedule { get; set; }

        
        public string FormattedDate
        {
            get
            {
                return string.Format("{0:MM/dd/yy}", date);
            }
        }
        [Required]
        public int covid { get; set; }



    }
}
