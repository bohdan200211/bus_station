using BusStation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using BusStation.DAO;

namespace BusStation.DataAccess
{
    public class BusAccess : DAOBus
    {
        public void Add(Bus bus)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = "insert into Bus (seats) values (" + bus.Seats + ")";
                var a = connection.Query<string>(query);
            }
        }

        public void Delete(long id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"delete from Bus where id = {id}";
                connection.Execute(query);
            }
        }

        public List<Bus> GetAll()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"select * from Bus order by id";
                var buses = connection.Query<Bus>(query).ToList();
                return buses;
            }
        }

        public List<Bus> GetManyBySelector(Predicate<Bus> match)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"select * from Bus order by id";
                var buses = connection.Query<Bus>(query).ToList();
                return buses.FindAll(match);
            }
        }

        public Bus GetOne(long id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"select * from Bus where id = " + id;
                var buses = connection.Query<Bus>(query).ToList();
                return buses[0];
            }
        }
        public void Update(Bus bus)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"update Bus set " +
                    $"seats = {bus.Seats} " +
                    $"where id = {bus.Id}";
                connection.Execute(query);
            }
        }
    }
}