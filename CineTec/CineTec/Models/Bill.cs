using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineTec.Models
{
    public class Bill
    {
        [ForeignKey("Client")]
        public int client_id { get; set; }

        [ForeignKey("Projection")]
        public int projection_id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string detail { get; set; }


    }
}
