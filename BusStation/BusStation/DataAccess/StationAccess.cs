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
    public class StationAccess : DAOStation
    {
        public void Add(Station station)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"insert into Station (name) values ('{station.name}')";
                connection.Execute(query);
            }
        }

        public void Delete(long id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = "delete from Station where Id = " + id;
                connection.Execute(query);
            }
        }

        public List<Station> GetAll()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"select * from Station order by id";
                var stations = connection.Query<Station>(query).ToList();
                return stations;
            }
        }

        public List<Station> GetManyBySelector(Predicate<Station> match)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"select * from Station order by id";
                var stations = connection.Query<Station>(query).ToList();
                return stations.FindAll(match);
            }
        }

        public Station GetOne(long id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"select * from Station where id = {id}";
                var stations = connection.Query<Station>(query).ToList();
                return stations[0];
            }
        }

        public void Update(Station station)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"update Station set " +
                    $"name = '{station.name}' " +
                    $"where id = {station.id}";
                connection.Execute(query);
            }
        }
    }
}
