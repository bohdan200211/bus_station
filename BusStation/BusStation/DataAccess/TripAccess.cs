using BusStation.DAO;
using BusStation.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStation.DataAccess
{
    public class TripAccess : DAOTrip
    {
        public void Add(Trip trip)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                if (this.checkAddTrip(trip)) {
                    string query = $"insert into Trip " +
                       $"(id_bus, datestart, dateend) " +
                       $"values " +
                       $"({trip.Bus.Id}, " +
                       $"CONVERT(datetime, '{trip.DateArrival.ToString("yyyy - MM - dd HH: mm")}',120), " +
                       $"CONVERT(datetime, '{trip.DateDeparture.ToString("yyyy - MM - dd HH: mm")}',120) )";
                    connection.Execute(query);
                }
            }
        }
        private bool checkAddTrip(Trip trip)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"select * from  Trip t where id_bus = 1 " +
                    $"and( " +
                        $"(convert(datetime,'{trip.DateArrival.ToString("yyyy-MM-dd HH:mm")}',120) < t.datestart and convert(datetime,'{trip.DateArrival.ToString("yyyy-MM-dd HH:mm")}',120) < t.datestart) " +
                        $"or " +
                        $"(convert(datetime,'{trip.DateDeparture.ToString("yyyy-MM-dd HH:mm")}',120) > t.dateend and convert(datetime,'{trip.DateDeparture.ToString("yyyy-MM-dd HH:mm")}',120) > t.dateend) " +
                    $") ";

                int all = this.GetAll().Count;
                int selectDate = connection.Query<int>(query).ToList().Count;

                if (all == selectDate) return true;
                return false;
            }
        }
        public void Delete(long id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"delete from Trip where id = {id}";
                connection.Execute(query);
            }
        }

        public List<Trip> GetAll()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"select * from Trip";
                List<Trip> trips = connection.Query<Trip>(query).ToList();
                return trips;
            }
        }

        public List<Trip> GetManyBySelector(Predicate<Trip> match)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"select * from Trip";
                List<Trip> trips = connection.Query<Trip>(query).ToList();
                return trips.FindAll(match);
            }
        }

        public Trip GetOne(long id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"select * from Trip where id = {id}";
                List<Trip> trips = connection.Query<Trip>(query).ToList();
                return trips[0];
            }
        }

        public List<Station> GetTripStations(long id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"select id_station, name from GetStations where id_trip = {id}";
                List<Station> stations = connection.Query<Station>(query).ToList();
                return stations;
            }
        }
        //Kostil
        class namedistanse
        {
            public string name { get; set; }
            public double distance { get; set; }
        }
        public double SearchDistance(Trip trip, string from, string to)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"select name, distance " +
                    $"from Station s inner join Stop stop " +
                    $"on s.id = stop.id_station " +
                    $"where stop.id_trip = {trip.Id} " +
                    $"order by stop.timestart";
                List<namedistanse> pairs = connection.Query<namedistanse>(query).ToList();
                double distance = 0;
                bool inRange = false;
                for(int i =0; i < pairs.Count; ++i)
                {
                    if(pairs[i].name.Trim() == from)
                    {
                        inRange = true;
                        continue;
                    }
                    if (inRange)
                    {
                        distance += pairs[i].distance;
                        
                        if (pairs[i].name.Trim() == to) 
                            break;
                    }
                }
                return distance;
            }
        }

            public List<Trip> SearchByStation(string from, string to, DateTime date)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"select t.id id,  max(t.id_bus) id_bus, max(t.datestart) datestart, max(t.dateend) dateend from Trip t inner join Stop stop on t.id = stop.id_trip " +
                    $"where " +
                        $"'{from}' in ( " +
                            $"select st.name from Stop s " +
                            $"inner join Station st on s.id_station = st.id " +
                            $"where s.id_trip = t.id " +
                        $") " +
                        $"and " +
                        $"'{to}' in (" +
                            $"select st.name from Stop s " +
                            $"inner join Station st on s.id_station = st.id " +
                            $"where s.id_trip = t.id" +
                        $")" +
                        $" and " +
                        $"convert(date, stop.timestart, 120) = convert(date, '{date.ToString("yyyy-MM-dd")}', 120) " +
                        $"group by t.id";
                List<Trip> trips = connection.Query<Trip>(query).ToList();
                return this.FromTo(trips, from, to);
            }
        }
        private List<Trip> FromTo(List<Trip> trips, string from, string to)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                for (int i = 0; i < trips.Count; ++i)
                {
                    string query = $"select name from Stop s inner join Station st on s.id_station = st.id where s.id_trip = {trips[i].Id} order by s.timestart";
                    var stations = connection.Query<string>(query).ToList();
                    if (stations.IndexOf(from) > stations.IndexOf(to))
                    {
                        trips.Remove(trips[i]);
                        --i;
                    }
                    else
                    {
                        query = $"select id, name from Stop s inner join Station st on s.id_station = st.id where s.id_trip = {trips[i].Id} order by s.timestart";
                        var stationsOrder = connection.Query<Station>(query).ToList();
                        trips[i].setStation(stationsOrder);
                    }
                }
                return trips;
            }
        }
    }
}
