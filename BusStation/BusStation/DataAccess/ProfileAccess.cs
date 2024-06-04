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
    public class ProfileAccess : DAOProfile
    {
        public Profile Add(Profile profile)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"insert into [Profile] " +
                    $"(id, firstname, lastname) " +
                    $"values ({profile.Id}, '{profile.FirstName}', '{profile.LastName}')";
             
                connection.Execute(query);
               
                return this.GetOne(profile.Id);
            }
        }

        public Profile GetOne(long id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = "select * from [Profile] where Id = " + id;
                var profiles = connection.Query<Profile>(query).ToList();
                return profiles[0];
            }
        }

        public void Update(Profile profile)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"update [Profile] set " +
                    $"firstname = '{profile.FirstName}', " +
                    $"lastname = '{profile.LastName}' " +
                    $"where id = {profile.Id}";

                connection.Execute(query);
            }
        }
    }
}

