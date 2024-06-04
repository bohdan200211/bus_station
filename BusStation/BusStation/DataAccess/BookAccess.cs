using BusStation.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using BusStation.Models;
using System.Data;

namespace BusStation.DataAccess
{
    public class BookAccess : DAOBook
    {
        public void Add(Book book)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"insert into Book " +
                    $"(id_user, id_trip, seat, id_document, " +
                    $"[from], [to], firstname, lastname, cost) " +
                    $"values " +
                    $"({book.IdUser}, {book.IdTrip}, {book.Seat}, {book.IdDocument}," +
                    $" '{book.From}', '{book.To}', '{book.Firstname}', '{book.Lastname}', " +
                    $"convert(decimal, {book.Cost}))";
                connection.Execute(query);
            }
        }

        public List<Book> GetAll()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"select id_user, id_trip, seat, id_document, [from], " +                 
                    $" [to], convert(decimal, cost) cost, firstname, lastname " +
                    $" from Book";
                var books = connection.Query<Book>(query).ToList();
                return books;
            }
        }
        public List<Book> GetByUserId(int id_user)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"select id_user, id_trip, seat, id_document, [from], " +
                                    $" [to], convert(decimal, cost) cost, firstname, lastname " +
                                    $" from Book" +
                      $" where id_user = {id_user}";
                var books = connection.Query<Book>(query).ToList();
                return books;
            }
        }
        public List<int> GetSeats(int id_trip)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.ConnValue("bus_station")))
            {
                string query = $"select seat from Book where id_trip = {id_trip}";
                var seats = connection.Query<int>(query).ToList();
                return seats;
            }
        }

    }
}
