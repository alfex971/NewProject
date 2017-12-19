using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishCatchesRestConsumer
{
    public class Catch
    {
       
        public int Id { get; set; }
   
        public string Name { get; set; }

        public string Species { get; set; }

        public double Weight { get; set; }

        public string Location { get; set; }

        public int Week { get; set; }
        public override string ToString()
        {
            return $"{Id},{Name},{Species},{Weight},{Location},{Week}";
        }
    }
}
