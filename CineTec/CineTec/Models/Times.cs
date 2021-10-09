using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CineTec.Models
{
    public class Times
    {
        [ForeignKey("Projection")]
        public int projection_id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public int schedule { get; set; }
    }
}
