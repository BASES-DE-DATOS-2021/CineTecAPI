using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace CineTec.Models
{
    public class Classification
    {
        [Key]
        public int Code { get; set; }
        public string Details { get; set; }
        public string Age_rating { get; set; }



    }
}
