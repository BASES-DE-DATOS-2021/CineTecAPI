using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CineTec.Models
{

    public class Seat
    {

        [ForeignKey("Room")]
        [Key, Column(Order = 0)]
        public int room_id { get; set; }
        [Key, Column(Order = 1)]
        public int number { get; set; }
        public string status { get; set; }

        public Seat(int room_id, int number, string status)
        {
            this.room_id = room_id;
            this.number = number;
            this.status = status;
        }


    }

}
