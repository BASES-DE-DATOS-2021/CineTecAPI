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
        public int row_quantity { get; set; }
        public int column_quantity { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]

        public int capacity { get; set; }


    }
}
