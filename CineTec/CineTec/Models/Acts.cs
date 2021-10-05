using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace CineTec.Models
{
    [Keyless]
    public class Acts
    {
        [ForeignKey("Movie")]
        public int movie_id { get; set; }

        [ForeignKey("Actor")]
        public int actor_id { get; set; }
    }
}
