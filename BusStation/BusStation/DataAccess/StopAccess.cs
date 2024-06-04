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
    public class StopAccess : DAOStop
    {
        public void Add(Stop stop)
        {
             using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                if (checkAddStop(stop.trip, stop.timestop))
                {
                    string query = $"insert into Stop " +
                        $"(id_trip, id_station,timestart,distance) " +
                        $"values " +
                        $"({stop.trip.Id}, " +
                        $"{stop.station.id}, " +
                        $"CONVERT(datetime, '{stop.timestop.ToString("yyyy-MM-dd HH:mm")}',120), " +
                        $"{stop.distance})";
                    connection.Execute(query);
                }
                else throw new Exception("Incorrect datetime stop");
            }
        }

        public bool checkAddStop(Trip trip, DateTime time)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"select * from Stop s," +
                    $"(select min(t.datestart) dstart, max(t.dateend) dend from Trip t " +
                        $"where t.id = 3006 " +
                    $") datetrip " +
                    $"where s.id_trip = {trip.Id} " +
                    $"and ( " +
                        $"convert(datetime, '{time.ToString("yyyy-MM-dd HH:mm")}',120) not in " +
                        $"(select s.timestart from Stop s where s.id_trip = {trip.Id}))" +
                    $"and " +
                    $"(datetrip.dstart " +
                    $"<= " +
                    $"convert(datetime, '{time.ToString( "yyyy-MM-dd HH:mm") }', 120) " +
                    $"and " +
                    $"convert(datetime, '{time.ToString("yyyy-MM-dd HH:mm")}', 120) " +
                    $"<= " +
                    $"datetrip.dend " +
                    $")";
                int all = this.GetAll().Count;
                int selectDate = connection.Query<int>(query).ToList().Count;

                if (all == selectDate) return true;
                return false;
            }
        }

        public void Delete(long id_trip, long id_station)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"delete from Stop where id_trip = {id_trip} AND id_station = {id_station}";
                connection.Execute(query);
            }
        }

        public List<Stop> GetAll()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"select * from Fullstop";
                var stops = connection.Query<Stop>(query).ToList();
                return stops;
            }
        }

        public List<Stop> GetManyBySelector(Predicate<Stop> match)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"select * from Fullstop order by timestop";
                var stops = connection.Query<Stop>(query).ToList();
                return stops.FindAll(match);
            }
        }

        public Stop GetOne(long id_trip, long id_station)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"select * from Fullstop where id_trip = {id_trip} AND id_station = {id_station}";
                var stops = connection.Query<Stop>(query).ToList();
                return stops[0];
            }
        }
    }
}
