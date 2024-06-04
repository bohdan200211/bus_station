using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStation.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string FirstName{ get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return this.LastName + " " + this.FirstName;
        }

    }
}
