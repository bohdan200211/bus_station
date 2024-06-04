using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStation.Models
{
    public class Book
    {
        public int IdUser { get; set; }
        public int IdTrip { get; set; }
        public int IdDocument { get; set; }
        public int Seat { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Decimal Cost { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public Book(int id_user, int id_trip, int seat, int id_document, string from, string to, Decimal cost, string firstname, string lastname)
        {
            this.IdUser = id_user;
            this.IdTrip = id_trip;
            this.Seat = seat;
            this.IdDocument = id_document;
            this.From = from.Trim();
            this.To = to.Trim();
            this.Cost = cost;
            this.Firstname = firstname.Trim();
            this.Lastname = lastname.Trim();
        }
    }
}
