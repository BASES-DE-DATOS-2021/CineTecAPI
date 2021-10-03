using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineTec.Models
{
    public class MovieComplete
    {

        public string classification_id { get; set; }
        public string image { get; set; }
        public string original_name { get; set; }
        public string name { get; set; }
        public string length { get; set; }
        public string director { get; set; }
        public IList<string> actors { get; set; }





    }
}
