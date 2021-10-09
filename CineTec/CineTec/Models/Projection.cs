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

        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime date { get; set; }


        public string FormattedDate
        {
            get
            {
                return string.Format("{0:dd/MM/yy}", date);
            }
        }



    }
}
