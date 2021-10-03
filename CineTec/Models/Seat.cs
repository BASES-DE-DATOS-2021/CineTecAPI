
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace CineTec.Models
{
    public class Seat
    {

        public int Room_id { get; set; }
        [Key]
        public int Number { get; set; }
        public string Status { get; set; }
  

    }
}
