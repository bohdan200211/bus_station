using BusStation.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStation.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public Bus Bus { get; set; }
        private List<Station> stations = new List<Station>();
        public DateTime DateArrival { get; set; }
        public DateTime DateDeparture { get; set; }
        public Trip(int id, int id_bus, DateTime datestart, DateTime dateend)
        {
            this.Id = id;
            this.stations = this.fillStation(this.Id);
            BusAccess db = new BusAccess();
            this.Bus = db.GetOne(id_bus);

            this.DateArrival = dateend;
            this.DateDeparture = datestart;
        }
        public List<Station> getStation()
        {
            return this.stations;
        }
        public void setStation(List<Station> stations)
        {
            this.stations = stations;
        }
        private List<Station> fillStation(long id)
        {
            TripAccess db = new TripAccess();
            var stations = db.GetTripStations(id);
            return stations;
        }
        public void Refresh()
        {
            this.stations = this.fillStation(this.Id);
        }
        public override string ToString()
        {
            return this.Id + "";
        }
        public bool CheckDateBeforeAdd(Trip trip) {
            trip.Refresh();
            var stations = trip.getStation();
 //           var time = DateTime.stations[stations.Count-1] - stations[0];
            return true;
        }
    }
}