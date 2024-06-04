using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStation.Models
{
    public class Bus
    {
        public int Id { get; set; }
        public int Seats { get; set; }
       
        public override string ToString()
        {
            return this.Id + "";
        }
    }
}
