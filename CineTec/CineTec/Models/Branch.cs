using System.ComponentModel.DataAnnotations;

namespace CineTec.Models
{
    public class Branch
    {
        [Key]
        public string cinema_name { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public int room_quantity { get; set; }

    }
}
