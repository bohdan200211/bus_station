using BusStation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStation.DAO
{
   public interface DAOTrip
    {
        Trip GetOne(long id);
        List<Station> GetTripStations(long id);
        List<Trip> GetAll();
        List<Trip> GetManyBySelector(Predicate<Trip> match);
        void Add(Trip trip);
        void Delete(long id);
        List<Trip> SearchByStation(string from, string to, DateTime date);
  //      KeyValuePair<string, string> dateMaxMin(Trip trip);
    }
}
