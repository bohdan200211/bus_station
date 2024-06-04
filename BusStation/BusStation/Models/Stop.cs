using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusStation.DataAccess;
namespace BusStation.Models
{
    public class Stop
    {
        public Trip trip { get; set; }
        public Station station { get; set; }
        public DateTime timestop { get; set; }
        public double distance { get; set; }
        public Stop(int id_trip, int id_station, string name_station, DateTime timestop, double distance)
        {
            TripAccess db = new TripAccess();
            this.trip = db.GetOne(id_trip);
            this.station = new Station { id = id_station, name = name_station.Trim() };
            this.timestop = timestop;
            this.distance = distance;
        }
    }
}