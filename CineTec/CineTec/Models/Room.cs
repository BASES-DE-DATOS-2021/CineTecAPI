using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineTec.Models
{
    public class Room
    {
        [ForeignKey("Branch")]
        public string branch_name { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public int row_quantity { get; set; }

        [Required]
        public int column_quantity { get; set; }

    }
}
