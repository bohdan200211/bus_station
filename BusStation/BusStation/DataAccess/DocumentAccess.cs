using BusStation.DAO;
using BusStation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace BusStation.DataAccess
{
    public class DocumentAccess : DAODocument
    {
        public void Add(Document document)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"insert into Document " +
                    $"(type, number, degree) " +
                    $"values " +
                    $"('{document.Type}', '{document.Number}', '{document.Degree}')";
                connection.Execute(query);
            }
        }

        public Document GetOne(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"select * from Document where id = {id}";
                var doc = connection.Query<Document>(query).ToList()[0];
                return doc;
            }
        }

        public Document GetOne(string series)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"select * from Document where number = '{series}'";
                var doc = connection.Query<Document>(query).ToList()[0];
                return doc;
            }
        }

        public void Update(Document document)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"update Document set " +
                    $"type = {document.Type} " +
                    $"number = {document.Number} " +
                    $"degree = {document.Degree} " +
                    $"where id = {document.Id}";
                connection.Execute(query);
            }
        }
    }
}
