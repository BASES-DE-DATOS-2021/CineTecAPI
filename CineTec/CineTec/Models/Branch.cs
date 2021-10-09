using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineTec.Models
{
    public class Branch
    {
        [Key]
        public string cinema_name { get; set; }

        [Required]
        public string province { get; set; }

        public string district { get; set; }

    }
}
