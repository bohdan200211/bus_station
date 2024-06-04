using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStation.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public Trip Trip { get; set; }
        public int Seat { get; set; }
        public Document Document { get; set; }
        public User User { get; set; }
    }
}
