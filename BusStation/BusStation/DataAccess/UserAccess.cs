using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusStation.DAO;
using BusStation.Models;
using Dapper;

namespace BusStation.DataAccess
{
    public class UserAccess : DAOUser
    {
        public List<User> GetAll()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = "select * from [Fulluser]";
                return connection.Query<User>(query).ToList();
            }
        }
        public List<User> GetManyBySelector(Predicate<User> match)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = "select * from [Fulluser]";
                var users = connection.Query<User>(query).ToList();
                return users.FindAll(match);
            }
        }
        public void Add(User user)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"insert into [User] " +
                    $"(username, password) " +
                    $"values ('{user.Username}', '{user.Password}')";
                connection.Execute(query);
            }
        }
        public User GetOne(long id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = "select * from [Fulluser] where Id = " + id;
                var users = connection.Query<User>(query).ToList();

                return users[0];
            }
        }

        public void UpdateUser(User user)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"update [User] set " +
                    $"username = '{user.Username}', " +
                    $"password = '{user.Password}' " +
                    $"where id = {user.Id}";
                connection.Execute(query);
            }
        }
    
        public void DeleteUser(long id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = "delete from [User] where Id = " + id;
                connection.Execute(query);
            }
        }
    }
}
