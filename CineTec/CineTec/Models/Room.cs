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
        [Range(6, 10)]
        public int row_quantity { get; set; }

        [Required]
        [Range(20, 25)]
        public int column_quantity { get; set; }

    }
}
