using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStation.Models
{
    public class Station
    {
        public int id { get; set; }
        private string Name;
        public string name { get { return Name; } set{ Name = value.Trim(); } }
        public override string ToString()
        {
            return name;
        }

    }
}
