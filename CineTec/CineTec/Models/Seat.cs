using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CineTec.Models
{
    [Keyless]
    public class Seat
    {
        [ForeignKey("Room")]
        public int room_id { get; set; }
        public int number { get; set; }
        public string status { get; set; }
  

    }
}
