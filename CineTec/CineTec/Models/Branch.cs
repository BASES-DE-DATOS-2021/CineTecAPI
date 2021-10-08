using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineTec.Models
{
    public class Branch
    {
        [Key]
        public string cinema_name { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public int rooms_quantity { get; set; }

    }
}
