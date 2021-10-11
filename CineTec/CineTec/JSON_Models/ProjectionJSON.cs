using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineTec.JSON_Models
{
    public class ProjectionJSON
    {
        public string movie { get; set; }

        public string date { get; set; }

        public List<string> schedule { get; set; }
    }
}
